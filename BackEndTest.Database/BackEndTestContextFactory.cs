using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace BackEndTest.Database
{
    class BackEndTestContextFactory : IDesignTimeDbContextFactory<BackEndTestContext>
    {
        public BackEndTestContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BackEndTestContext>();
            var connectionString = configuration.GetConnectionString("backend-test");
            builder.UseSqlite(connectionString);

            return new BackEndTestContext(builder.Options);
        }
    }
}
