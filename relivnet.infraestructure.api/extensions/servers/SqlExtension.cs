using Microsoft.EntityFrameworkCore;
using relivnet.infraestructure.azsql.contexts;

namespace relivnet.infraestructure.api.extensions.servers
{
    public class SqlExtension
    {
        public static void ConfigureSQLServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RelivnetDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("relivnetConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("relivnet.infraestructure.azsql");
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(90), errorNumbersToAdd: null);
                    });
                }, ServiceLifetime.Transient);
        }
    }
}
