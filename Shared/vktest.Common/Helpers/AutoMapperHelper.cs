using Microsoft.Extensions.DependencyInjection;

namespace vktest.Common.Helpers
{
    public static class AutoMappersRegisterHelper
    {
        public static void Register(IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(s => s.FullName != null && (s.FullName.ToLower().StartsWith("vktest.") ||  s.FullName.ToLower().StartsWith("vktest.Api")));

            services.AddAutoMapper(assemblies);
        }
    }
}
