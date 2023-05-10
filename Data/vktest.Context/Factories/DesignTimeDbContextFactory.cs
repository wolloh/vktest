using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using vktest.Context.Settings;

namespace vktest.Context.Factories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        private const string migrationProjctPrefix = "vktest.Context.Migrations";

        public MainDbContext CreateDbContext(string[] args)
        {
            var provider = (args?[0] ?? $"{DbType.PostgreSQL}").ToLower();

            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.context.json")
                 .Build();


            DbContextOptions<MainDbContext> options;
            if (provider.Equals($"{DbType.PostgreSQL}".ToLower()))
            {
                options = new DbContextOptionsBuilder<MainDbContext>()
                        .UseNpgsql(
                            configuration.GetConnectionString(provider),
                            opts => opts
                                .MigrationsAssembly($"{migrationProjctPrefix}{DbType.PostgreSQL}")
                        )
                        .Options;
            }
            else
            {
                throw new Exception($"Unsupported provider: {provider}");
            }

            var dbf = new DbContextFactory(options);
            return dbf.Create();
        }
    }
}
