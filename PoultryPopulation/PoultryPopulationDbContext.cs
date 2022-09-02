using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PoultryPopulation.Entities;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryPopulation
{
    public class PoultryPopulationDbContext : DbContext
    {
        public PoultryPopulationDbContext() : base(GetOptions()) {
        }

        private static DbContextOptions<PoultryPopulationDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<PoultryPopulationDbContext>()
                .UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=PoultryPopulation;Trusted_Connection=True;").Options;
        }

        public PoultryPopulationDbContext(IConfiguration configuration) : base(GetOptions(configuration)) { }

        public DbSet<Chicken> Chickens { get; set; }
        public DbSet<ChickenBreed> ChickenBreeds { get; set; }
        public DbSet<ChickenCoop> ChickenCoops { get; set; }
        public DbSet<Owner> Owners { get; set; }
        

        private static DbContextOptions<PoultryPopulationDbContext> GetOptions(IConfiguration configuration)
        {
            return new DbContextOptionsBuilder<PoultryPopulationDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        private void OnSaveChanges()
        {
            if (ChangeTracker.HasChanges())
            {
                foreach (var entry in ChangeTracker.Entries<ICreatedModified>().ToList())
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (entry.Entity.Created == default)
                            {
                                entry.Entity.Created = now;
                            }
                            if (entry.Entity.Modified == default)
                            {
                                entry.Entity.Modified = now;
                            }
                            break;
                        case EntityState.Modified:
                            entry.Entity.Modified = now;
                            break;
                    }
                }
            }
        }

        [Obsolete("Use SaveAsync", error: true)]
        public override int SaveChanges()
        {
            OnSaveChanges();
            return base.SaveChanges();
        }

        public async Task SaveAsync()
        {
            OnSaveChanges();
            _ = await base.SaveChangesAsync();
        }
    }
}
