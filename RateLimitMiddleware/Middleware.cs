using System.Net.Http;

namespace RateLimitMiddleware
{
    public class RateLimitMiddleware
    {
        private readonly ILogger<RateLimitMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IRateLimitService _rateLimitService;

        public RateLimitMiddleware(RequestDelegate next, IRateLimitService rateLimitService, ILogger<RateLimitMiddleware> logger)
        {
            _next = next;
            _rateLimitService = rateLimitService;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            // Implement rate limiting logic using _rateLimitService
            if (!_rateLimitService.IsAllowed(context.Request))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded");
                return;
            }
            await _next(context);
        }
    }
}