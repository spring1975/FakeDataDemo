using System.Collections.Generic;
using System.Linq;
using AutoBogus;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PoultryPopulation.Entities;
using PoultryPopulation.ViewModels;
using Utilities;

namespace PoultryPopulation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PoultryPopulationController : ControllerBase
    {
        private readonly ILogger<PoultryPopulationController> _logger;
        private readonly IConfiguration _configuration;

        public PoultryPopulationController(ILogger<PoultryPopulationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("all-chickens-from-json")]
        public IEnumerable<Chicken> RoosterRosterFromJson()
        {
            return PayloadJsonSerializationHelpers
                .FromJson<Chicken[]>(@"FakeData\all-poultry\poultry-census.json").Take(10).ToArray();
        }

        [HttpGet("rooster-roster")]
        public IEnumerable<RoosterRoster> RoosterRoster()
        {
            Faker<RoosterRoster> fakeRoster = new AutoFaker<RoosterRoster>()
                    .UseSeed(123456)
                    .RuleFor(r => r.ShiftStart, f => f.Date.Between(Bogus.DataSets.Date.SystemClock(), f.Date.Future()))
                    .RuleFor(r => r.ShiftEnd, (f, r) => f.Date.Between(r.ShiftStart.AddMinutes(30), r.ShiftStart.AddHours(8)))
                    .RuleFor(r => r.ChickenCoopName, f => f.Company.CompanyName())
                    .RuleFor(r => r.RoosterName, f => f.Name.FirstName(Name.Gender.Male))
                    .RuleFor(r => r.CoopLocation, f => $"{f.Address.Latitude()}, {f.Address.Longitude()}")
                ;
            return fakeRoster.Generate(100).OrderBy(r => r.ShiftStart);
        }

        [HttpGet("rooster-roll-call")]
        public IEnumerable<Chicken> RoosterRollCall()
        {
            using var db = new PoultryPopulationDbContext(_configuration);
            return db.Chickens.Where(c => c.Gender == Gender.Male).ToList();
        }
    }
}
