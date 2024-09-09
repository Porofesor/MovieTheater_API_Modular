using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Tickets.Core.Abstractions;
using Modules.Tickets.Infrastructure.Persistence;
using Shared.Infrastructure.Extensions;

namespace Modules.Tickets.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTicketInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<TicketDbContext>(config)
                .AddScoped<ITicketDbContext>(provider => provider.GetService<TicketDbContext>());
            return services;
        }
    }
}
