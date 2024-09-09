using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Tickets.Core.Extensions;
using Modules.Tickets.Infrastructure.Extension;

namespace Modules.Tickets.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddTicketModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTicketCore()
                .AddTicketInfrastructure(configuration);
            return services;
        }
    }
}
