using Modules.Movies.Infrastructure.Extensions;
using Modules.MovieTheater.Infrastructure.Extensions;

namespace WEB_API.Infrastructure
{
    public static class UnitOfWorkServices
    {
        public static IServiceCollection AddUnitOfWorkServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMoviesUnitOfWork(configuration);
            services.AddMoviesTheaterUnitOfWork(configuration);
            return services;
        }
    }
}
