using AutoMapper.Profiles.Profiles.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapper.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapperCore(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(MovieProfiles) /**/);
            return services;
        }
    }
}
