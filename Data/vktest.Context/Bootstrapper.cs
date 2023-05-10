using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vktest.Settings;
using vktest.Context.Settings;
using vktest.Context.Factories;

namespace vktest.Context
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Register db context
        /// </summary>
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration = null)
        {
            var settings = vktest.Settings.Settings.Load<DbSettings>("Database", configuration);
            services.AddSingleton(settings);

            var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(
                settings.ConnectionString,
                settings.Type);

            services.AddDbContextFactory<MainDbContext>(dbInitOptionsDelegate);

            return services;
        }
    }
}
