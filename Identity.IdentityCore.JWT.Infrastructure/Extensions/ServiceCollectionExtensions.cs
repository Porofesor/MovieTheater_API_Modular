using Identity.IdentityCore.JWT.Abstractions;
using Identity.IdentityCore.JWT.Infrastructure.Infrastructure;
using Identity.IdentityCore.JWT.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Infrastructure.Extensions;
using System.Text;

namespace Identity.IdentityCore.JWT.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTicketInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<UsersDbContext>(config)
                .AddScoped<IUsersDbContext>(provider => provider.GetService<UsersDbContext>());

            
            return services;
        }

        /// <summary>
        /// Adds the necessary JWT infrastructure services to the IServiceCollection.
        /// This includes the TokenProvider for creating JWT tokens and PasswordHasher for hashing and verifying passwords.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The updated IServiceCollection with JWT infrastructure services added.</returns>
        public static IServiceCollection AddJWTInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<TokenProvider>();
            services.AddSingleton<PasswordHasher>();

            return services;
        }

        /// <summary>
        /// Adds JWT authentication to the service collection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="configuration">The IConfiguration to read the JWT settings from.</param>
        /// <returns>The IServiceCollection with JWT authentication added.</returns>
        public static IServiceCollection AddAuthenticationJWT(this IServiceCollection services, IConfiguration configuration)
        {
            // Get JWT configuration settings from appsettings.json or environment variables
            var jwtSettingsKey = Environment.GetEnvironmentVariable("JWT_SETTINGS_KEY");
            var jwtSettingsIssuer = Environment.GetEnvironmentVariable("JWT_SETTINGS_ISSUER");
            var jwtSettingsAudience = Environment.GetEnvironmentVariable("JWT_SETTINGS_AUDIENCE");

            if (string.IsNullOrEmpty(jwtSettingsKey) || string.IsNullOrEmpty(jwtSettingsIssuer) || string.IsNullOrEmpty(jwtSettingsAudience))
            {
                throw new ArgumentNullException("JWT settings are missing in the configuration.");
            }

            // Configure JWT authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;  // TODO: Should be true for production
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettingsKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettingsIssuer,
                        ValidateAudience = true,
                        ValidAudience = jwtSettingsAudience,
                        ValidateLifetime = true, // Validate token expiration
                        ClockSkew = TimeSpan.Zero // Reduce the allowed clock skew to prevent token abuse
                    };

                    // Handle JWT token validation failures
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
