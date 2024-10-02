using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Swagger.Identity.JWT
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures Swagger with JWT Bearer authentication support.
        /// </summary>
        /// <param name="services">The IServiceCollection to add Swagger with authentication support.</param>
        /// <returns>The IServiceCollection with Swagger and JWT Bearer authentication configured.</returns>
        public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                // Use full class names for schema IDs to avoid naming conflicts
                c.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                // Define the JWT security scheme
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token in this field.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                };

                // Add the security definition to Swagger
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

                // Ensure the JWT token is required for accessing secured endpoints
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] {}
                }
            });
            });
        }
    }
}
