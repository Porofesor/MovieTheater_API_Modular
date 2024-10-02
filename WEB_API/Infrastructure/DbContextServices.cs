using Modules.Movies.Extensions;
using Modules.Tickets.Extensions;

namespace WEB_API.Infrastructure
{
    public static class DbContextServices
    {
        public static IServiceCollection AddDbContextServises(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTicketModule(configuration);
            services.AddMoviesModule(configuration);
            return services;
        }
    }
}
