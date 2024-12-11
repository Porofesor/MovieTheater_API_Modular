using Modules.Movies.Extensions;
using Modules.Tickets.Extensions;

namespace WEB_API.Infrastructure
{
    public static class ModuleServices
    {
        public static IServiceCollection AddModuleServises(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTicketModule(configuration);
            services.AddMoviesModule(configuration);
            //services.AddUsersModule(configuration);
            return services;
        }
    }
}
