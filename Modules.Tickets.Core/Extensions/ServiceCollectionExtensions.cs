using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Modules.Tickets.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTicketCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
