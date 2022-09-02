using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace PoultryPopulation.Seeder.Services
{
    public class EntityFakerService
    {
        private readonly EntityFakes _entityFakes;
        private readonly ILogger<EntityFakerService> _logger;

        public EntityFakerService(ILoggerFactory loggerFactory, EntityFakes entityFakes)
        {
            _entityFakes = entityFakes;
            _logger = loggerFactory.CreateLogger<EntityFakerService>();
        }

        public EntityCollections Generate()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            _logger.LogInformation("Generating Seed Data");
            _entityFakes.GenerateAll();
            _logger.LogInformation($"Generated Seed Data in {sw.Elapsed} ");
            return _entityFakes;
        }
    }
}
