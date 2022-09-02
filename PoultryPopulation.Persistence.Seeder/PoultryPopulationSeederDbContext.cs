using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PoultryPopulation.Entities;

namespace PoultryPopulation.Seeder
{
    public class PoultryPopulationSeederDbContext: PoultryPopulationDbContext
    {
        private static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddDebug();
        });

        public PoultryPopulationSeederDbContext(IConfiguration configuration) : base(configuration) { }

        internal void Seed<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            this.BulkInsert(entities.ToImmutableList());
            var tableName = Model.FindEntityType(typeof(TEntity)).GetTableName();
            Console.WriteLine($"Seeded { tableName }");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseLoggerFactory(DbLoggerFactory);
#endif
        }

        public DbSet<Address> Address { get; set; }

    }
}
