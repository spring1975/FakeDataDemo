using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PoultryPopulation.Entities;
using Utilities;

namespace PoultryPopulation
{
    public class PoultryRollCall: IDisposable
    {
        private PoultryPopulationDbContext _db;

        public PoultryRollCall(IConfiguration configuration)
        {
            _db = new PoultryPopulationDbContext(configuration);
        }

        public void ListAll()
        {
            List<Chicken> chickens = _db.Chickens.ToList();

            var json = PayloadJsonSerializationHelpers.ToJSON(chickens);

            Console.WriteLine(json);
        }

        

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
