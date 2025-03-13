using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.MovieTheater.Core.Extensions;
using Modules.MovieTheater.Infrastructure.Extensions;
namespace Modules.MovieTheater.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddMovieTheaterModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMovieTheaterCore()
                .AddMovieTheaterInfrastructure(configuration);
            return services;
        }
    }
}
