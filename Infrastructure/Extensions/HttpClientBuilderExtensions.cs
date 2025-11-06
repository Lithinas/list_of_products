using Polly;

namespace List_of_products.Infrastructure.Extensions
{
    public static class HttpClientBuilderExtensions
    {
        /// <summary>
        /// Adds standard resilience policies to the HttpClient:
        /// - Retry policy with exponential backoff (3 retries: 2s, 4s, 8s)
        /// - Circuit breaker (opens after 5 failures, stays open for 30s)
        /// </summary>
        public static IHttpClientBuilder AddStandardResiliencePolicies(this IHttpClientBuilder builder)
        {
            return builder
                .AddTransientHttpErrorPolicy(policy =>
                    policy.WaitAndRetryAsync(3, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
                .AddTransientHttpErrorPolicy(policy =>
                    policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        }
    }
}
