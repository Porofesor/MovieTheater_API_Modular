using Identity.IdentityCore.JWT.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.IdentityCore
{
    public static class ModuleExtensions
    {
        /// <summary>
        /// Adds JWT identification, incudes: authentication, PasswordHasher, TokenProvider, UserDBContext
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="configuration">The IConfiguration to read the JWT settings from.</param>
        /// <returns>The IServiceCollection with JWT authentication added.</returns>
        public static IServiceCollection AddIdentityJWTModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddUsersInfrastructure(configuration);
            return services;
        }
    }
}
