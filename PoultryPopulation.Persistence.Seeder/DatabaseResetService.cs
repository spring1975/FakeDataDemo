using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PoultryPopulation.Seeder
{
    public class DatabaseResetService
    {
        private readonly ILogger<DatabaseResetService> _logger;
        private readonly IConfiguration _configuration;

        public DatabaseResetService(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<DatabaseResetService>();
            _configuration = configuration;
        }

        internal void ResetDb()
        {
            var sw = new Stopwatch();
            sw.Start();

            // NOTE: This does not use the seeder context
            using var migrationContext = new PoultryPopulationDbContext(_configuration);
            using var masterContext = new MasterDbContext(_configuration);

#if DEBUG
            /***************************WARNING****************************
             * Run this with care. It is handy when bouncing between branches
             * have that very separate migration scripts and in this case, we're
             * seeding the database from scratch anyway. However, dropping the
             * database is very risky.
             * */
            _logger.LogInformation("Deleting database, if exists.");
            migrationContext.Database.EnsureDeleted();
            _logger.LogInformation(
                $"Database deleted in {sw.Elapsed} ");
#endif

            sw.Restart();

            _logger.LogInformation($"Migrating {migrationContext.Database.GetDbConnection().Database} on {migrationContext.Database.GetDbConnection().DataSource}");
            migrationContext.Database.Migrate();
            _logger.LogInformation(
                $"Migration Complete in {sw.Elapsed} ");

            sw.Restart();

            _logger.LogInformation("Ensuring Database User...");
            var createUserOptions = new CreateUserOptions();
            _configuration.Bind("AppUser", createUserOptions);
            masterContext.Database.EnsureServerLogin(createUserOptions);
            migrationContext.Database.EnsureUser(createUserOptions);
            _logger.LogInformation(
                $"Database User command run in {sw.Elapsed} ");
        }
    }
}