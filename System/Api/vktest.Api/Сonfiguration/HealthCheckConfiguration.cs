using Api.Configuration.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using vktest.Common.HealthChecks;

namespace Api.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<SelfHealthCheck>("Api");

            return services;
        }

        public static void UseAppHealthChecks(this WebApplication app)
        {
            app.MapHealthChecks("/health");

            app.MapHealthChecks("/health/detail", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckHelper.WriteHealthCheckResponse,
                AllowCachingResponses = false,
            });
        }
    }
}
