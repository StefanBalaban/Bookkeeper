using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tulumba.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class TulumbaDbContextFactory : IDesignTimeDbContextFactory<TulumbaDbContext>
    {
        public TulumbaDbContext CreateDbContext(string[] args)
        {
            TulumbaEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<TulumbaDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"));

            return new TulumbaDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Tulumba.DbMigrator/"))
                .AddJsonFile("appsettings.json", false);

            return builder.Build();
        }
    }
}