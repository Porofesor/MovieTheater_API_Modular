using Modules.Movies.Extensions;
using Modules.Tickets.Extensions;
using Modules.MovieTheater.Extensions;

namespace WEB_API.Infrastructure
{
    public static class ModuleServices
    {
        public static IServiceCollection AddModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTicketModule(configuration);
            //services.AddMoviesModule(configuration);
            //services.AddUsersModule(configuration);
            services.AddMovieTheaterModule(configuration);
            return services;
        }
    }
}
