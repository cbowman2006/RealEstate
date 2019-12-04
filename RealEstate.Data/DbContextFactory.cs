using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RealEstate.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<RealEstateDbContext>
    {
        public RealEstateDbContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var Configuration = configBuilder.Build();
            var builder = new DbContextOptionsBuilder<RealEstateDbContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RealEstate;Trusted_Connection=True;MultipleActiveResultSets=true",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(RealEstateDbContext).GetTypeInfo().Assembly.GetName().Name));                     
            return new RealEstateDbContext(builder.Options);
        }
        
    }
}