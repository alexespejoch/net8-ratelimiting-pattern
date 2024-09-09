using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace BookStore.WebApi.Configuration
{
    public static class PresentationDependencies
    {
        public static IServiceCollection RegisterPresentationDependencies(this IServiceCollection services)
        {
            var fixedWindowPolicy = "fixedWindow";
            services.AddRateLimiter(configureOptions => {
                configureOptions.AddFixedWindowLimiter(policyName: fixedWindowPolicy, options =>
                {
                    options.PermitLimit = 4;
                    options.Window = TimeSpan.FromSeconds(30);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 0;
                });
                configureOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            var concurrencyPolicy = "Concurrency";
            services.AddRateLimiter(configureOptions =>
            {
                configureOptions.AddConcurrencyLimiter(policyName: concurrencyPolicy, options =>
                {
                    options.PermitLimit = 2;
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 0;
                });
            });

            return services;
        }
    }
}
