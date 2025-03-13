using AspNetCoreRateLimit;

namespace RateLimitServices
{
    public static class RateLimitServices
    {
        public static IServiceCollection AddRateLimitServisec(this IServiceCollection services, IConfiguration configuration)
        {
            // Required for in-memory rate limit storage
            services.AddMemoryCache();

            // Load rate limiting configuration from appsettings.json
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            // Register the rate limit services
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();

            return services;
        }
    }
}