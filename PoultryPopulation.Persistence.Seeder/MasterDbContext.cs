using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PoultryPopulation.Seeder
{
    internal class MasterDbContext : DbContext
    {
        private static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder =>
        {
            //builder.AddDebug();
        });

        public MasterDbContext(IConfiguration configuration) : base(GetOptions(configuration)) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(DbLoggerFactory);
        }

        private static DbContextOptions<MasterDbContext> GetOptions(IConfiguration configuration)
        {
            return new DbContextOptionsBuilder<MasterDbContext>()
                .UseSqlServer(configuration.GetConnectionString("MasterConnection")).Options;
        }
    }
}
