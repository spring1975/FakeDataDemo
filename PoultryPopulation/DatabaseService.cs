using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PoultryPopulation
{
    public class DatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureDb()
        {
            using var context = new PoultryPopulationDbContext(_configuration);
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
}