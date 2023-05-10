using vktest.Services.Movies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vktest.Services.Movies
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUserService(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();

            return services;
        }
    }
}
