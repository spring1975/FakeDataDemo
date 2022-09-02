using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PoultryPopulation.Seeder.Services
{
    internal class DataSeederService
    {
        private PoultryPopulationSeederDbContext _db;
        private readonly IConfiguration _configuration;

        public DataSeederService(IConfiguration configuration)
        {
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
                _db.Seed(entityCollections.ChickenBreeds);
                _db.Seed(entityCollections.Chickens);
                _db.Seed(entityCollections.ChickenCoops);
                _db.Seed(entityCollections.Owners);
                _db.Seed(entityCollections.Addresses);

                Console.WriteLine($"Seed data saved to tables in {sw.Elapsed} ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _db.Database.CloseConnection();
            }
        }
    }
}
