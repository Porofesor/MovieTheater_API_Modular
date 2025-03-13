using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.MovieTheater.Core.Abstractions;
using Modules.MovieTheater.Infrastructure.Persistance;
using Modules.MovieTheater.Infrastructure.UnitOfWork;
using Shared.Infrastructure.Extensions;

namespace Modules.MovieTheater.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieTheaterInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<MovieTheaterDbContext>(config)
                .AddScoped<IMovieTheaterUnitOfWork,MovieTheaterUnitOfWork>()
                .AddScoped<IMovieTheaterDbContext>(provider =>  provider.GetService<MovieTheaterDbContext>());
            return services;
        }
    }
}
