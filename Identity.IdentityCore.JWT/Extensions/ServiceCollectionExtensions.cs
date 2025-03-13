using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.IdentityCore.JWT.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityJWTCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
