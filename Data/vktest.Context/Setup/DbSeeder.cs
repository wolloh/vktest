using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vktest.Common.Enums;

namespace vktest.Context.Setup
{
    public static class DbSeeder
    {
        private static IServiceScope ServiceScope(IServiceProvider serviceProvider) => serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
        private static MainDbContext DbContext(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();


        public static void Execute(IServiceProvider serviceProvider, bool addDemoData, bool addAdmin = true)
        {
            using var scope = ServiceScope(serviceProvider);
            ArgumentNullException.ThrowIfNull(scope);

            if (addDemoData)
            {
                Task.Run(async () =>
                {
                    await ConfigureDemoData(serviceProvider);
                });
            }
        }

        private static async Task ConfigureDemoData(IServiceProvider serviceProvider)
        {
            await AddUsers(serviceProvider);
        }

        private static async Task AddUsers(IServiceProvider serviceProvider)
        {
            await using var context = DbContext(serviceProvider);


            if (context.UserGroups.Any() || context.UserStates.Any() || context.Users.Any())
                return;

            var g1 = new Entities.User_Group()
            {
                Code=UserType.Admin,
                Description="Admin can perform all operations"
            };
            var g2 = new Entities.User_Group()
            {
                Code = UserType.User,
                Description = "User can perform basic operations"
            };
            context.UserGroups.Add(g1);
            context.UserGroups.Add(g2);
            var st1 = new Entities.User_State()
            {
                Code=UserState.Active,
                Description="Active user"
            };
            var st2 = new Entities.User_State()
            {
                Code = UserState.Blocked,
                Description="Blocked user"
            };
            context.UserStates.Add(st1);
            context.UserStates.Add(st2);


            var usr1 = new Entities.User()
            {
                Login="login",
                Password="password",
                Created_Date=DateTime.Now,
                Group=g1,
                State=st1
            };
            var usr2 = new Entities.User()
            {
                Login = "login2",
                Password = "password2",
                Created_Date = DateTime.Now,
                Group = g2,
                State = st1
            };
            context.Users.Add(usr1);
            context.Users.Add(usr2);
            context.SaveChanges();
        }
    }
}
