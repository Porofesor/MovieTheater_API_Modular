using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Modules.MovieTheater.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieTheaterCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
