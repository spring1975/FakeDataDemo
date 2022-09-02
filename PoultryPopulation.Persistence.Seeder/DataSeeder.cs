using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PoultryPopulation.Seeder
{
    public class DataSeeder : IDisposable
    {
        private PoultryPopulationSeederDbContext _db;
        private readonly ILogger<DataSeeder> _logger;
        private readonly IConfiguration _configuration;

        public DataSeeder(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<DataSeeder>();
            _configuration = configuration;
        }

        public void Seed(EntityCollections entityCollections)
        {
            var sw = new Stopwatch();
            sw.Start();
            _db = new PoultryPopulationSeederDbContext(_configuration);
            _db.Database.OpenConnection();

            try
            {
                sw.Restart();
                _db.Seed(entityCollections.Owners);
                _db.Seed(entityCollections.Addresses);
                _db.Seed(entityCollections.ChickenBreeds);
                _db.Seed(entityCollections.ChickenCoops);
                _db.Seed(entityCollections.Chickens);
                

                _logger.LogInformation($"Seed data saved to tables in {sw.Elapsed} ");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
            finally
            {
                _db.Database.CloseConnection();
            }
        }

        public void Dispose()
        {
            _db?.Dispose();
    }
}
}
