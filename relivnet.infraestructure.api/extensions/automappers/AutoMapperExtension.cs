using System.Reflection;
using relivnet.infraestructure.application.profiles;

namespace relivnet.infraestructure.api.extensions.automappers
{
    public static class AutoMapperExtension
    {
        public static void ConfigureAutoMappersServices(IServiceCollection service)
        {
            service.AddAutoMapper(typeof(UserProfile).GetTypeInfo().Assembly);
        }
    }
}
