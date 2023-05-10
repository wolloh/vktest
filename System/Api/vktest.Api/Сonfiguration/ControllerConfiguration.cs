using Api.Configuration;
using vktest.Common.Extensions;
using Microsoft.Extensions.Options;

namespace vktest.Api.Configuration
{
    public static class ControllerConfiguration
    {
        public static IServiceCollection AddAppController(this IServiceCollection services)
        {

            services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultSettings())
                .AddValidator()
                ;

            return services;
        }
        public static IEndpointRouteBuilder UseAppController(this IEndpointRouteBuilder app)
        {
            app.MapControllers();

            return app;
        }
    }
}
