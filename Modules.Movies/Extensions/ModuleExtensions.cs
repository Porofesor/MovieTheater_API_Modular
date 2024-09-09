using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Movies.Infrastructure.Extensions;
using Modules.Tickets.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Movies.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddMoviesModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMoviesCore()
                .AddMoviesInfrastructure(configuration);
            return services;
        }
    }
}
