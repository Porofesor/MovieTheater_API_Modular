using Modules.Movies.Infrastructure.Extensions;

namespace WEB_API.Infrastructure
{
    public static class UnitOfWorkServices
    {
        public static IServiceCollection AddUnitOfWorkServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMoviesUnitOfWork(configuration);
            return services;
        }
    }
}
