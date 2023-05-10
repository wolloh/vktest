using vktest.Services.Movies;

namespace vktest.Api
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services
                //.AddMainSettings()
                .AddUserService()
                //.AddIdentitySettings()
                ;

            return services;
        }
    }
}
