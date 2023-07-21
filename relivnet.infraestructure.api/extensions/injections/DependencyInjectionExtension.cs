

using relivnet.domain.repositories;
using relivnet.infraestructure.application;
using relivnet.infraestructure.azsql.repositories.category;
using relivnet.infraestructure.azsql.repositories.products;
using relivnet.infraestructure.azsql.repositories.state;
using relivnet.infraestructure.azsql.repositories.users;

namespace relivnet.infraestructure.api.extensions.injections
{
    public class DependencyInjectionExtension
    {
        public static void ConfigureDependenciesInjectionsServices(WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddScoped<IUserDomainRepository, UserRepository>();
            builder.Services.AddScoped<IRoleDomainRepository, RoleRepository>();
            builder.Services.AddScoped<ICategoryDomainRepository, CategoryRepository>();
            builder.Services.AddScoped<IStateDomainRepository, StateRepository>();
            builder.Services.AddScoped<IProductDomainRepository, ProductRepository>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
