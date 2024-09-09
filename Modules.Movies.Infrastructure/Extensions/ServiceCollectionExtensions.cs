using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Movies.Core.Abstractions;
using Modules.Movies.Infrastructure.Persistence;
using Modules.Movies.Infrastructure.UnitOfWork;
using Shared.Infrastructure.Extensions;

namespace Modules.Movies.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMoviesInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<MoviesDbContext>(config)
                .AddScoped<IMoviesDbContext>(provider => provider.GetService<MoviesDbContext>());
            return services;
        }
        public static IServiceCollection AddMoviesUnitOfWork(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IMovieUnitOfWork,MovieUnitOfWork>();
            return services;
        }
    }
}
