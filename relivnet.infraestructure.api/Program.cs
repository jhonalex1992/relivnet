using System.Security.Claims;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using relivnet.domain.models;
using relivnet.infraestructure.api.extensions.automappers;
using relivnet.infraestructure.api.extensions.injections;
using relivnet.infraestructure.api.extensions.migrations;
using relivnet.infraestructure.api.extensions.securities;
using relivnet.infraestructure.api.extensions.servers;
using relivnet.infraestructure.api.middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    b.Register(context =>
        {
            ClaimsPrincipal? identityUser = context.Resolve<IHttpContextAccessor>()?.HttpContext?.User;

            Claim? data = ((ClaimsIdentity)identityUser?.Identity!)?.Claims.FirstOrDefault(x => x.Type == "id");

            UserInfoModel userInfo = identityUser != null && data != null ? new UserInfoModel()
            {
                Id = identityUser.Identity != null && identityUser.Identity.IsAuthenticated ? Convert.ToInt32(((ClaimsIdentity)identityUser.Identity).Claims.FirstOrDefault(x => x.Type == "id")?.Value) : 0,
                UserName = identityUser.Identity != null && identityUser.Identity.IsAuthenticated ? ((ClaimsIdentity)identityUser.Identity).Claims.FirstOrDefault(x => x.Type == "username")?.Value : String.Empty,
            } : new UserInfoModel() { Id = 0, UserName = "" };
            return userInfo;
        }).AsSelf()
        .InstancePerLifetimeScope();

});

builder.Services.AddCors(p => p.AddPolicy("corsDev", configurePolice =>
{
    configurePolice.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Add services to the container.
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
    .Build();

builder.Services.AddControllers();
AutoMapperExtension.ConfigureAutoMappersServices(builder.Services);
DependencyInjectionExtension.ConfigureDependenciesInjectionsServices(builder, configuration);
SqlExtension.ConfigureSQLServices(builder);
JwtExtension.ConfigureSecurityServices(builder);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();



app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseCors("corsDev");
}

app.Run();


