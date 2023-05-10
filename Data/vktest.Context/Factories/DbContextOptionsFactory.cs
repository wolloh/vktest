using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vktest.Context.Settings;

namespace vktest.Context.Factories
{
    public static class DbContextOptionsFactory
    {
        private const string migrationProjctPrefix = "vktest.Context.Migrations";

        public static DbContextOptions<MainDbContext> Create(string connStr, DbType dbType)
        {
            var bldr = new DbContextOptionsBuilder<MainDbContext>();

            Configure(connStr, dbType).Invoke(bldr);

            return bldr.Options;
        }

        public static Action<DbContextOptionsBuilder> Configure(string connStr, DbType dbType)
        {
            return (bldr) =>
            {
                switch (dbType)
                {

                    case DbType.PostgreSQL:
                        bldr.UseNpgsql(connStr,
                            opts => opts
                                    .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                                    .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                                    .MigrationsAssembly($"{migrationProjctPrefix}{DbType.PostgreSQL}")
                            );
                        break;
                }

                bldr.EnableSensitiveDataLogging();
                bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            };
        }
    }
}
