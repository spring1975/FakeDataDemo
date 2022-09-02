using System;
using AutoBogus;
using AutoBogus.Conventions;
using PoultryCloningFactory.Fakes;
using PoultryCloningFactory.ModelAutoGeneratorConventions;
using PoultryCloningFactory.ModelAutoGeneratorOverrides;

namespace PoultryPopulation.Seeder
{
    public class EntityFakes : EntityCollections
    {
        private readonly AddressFaker _addressFaker;
        private readonly ChickenBreedFaker _chickenBreedFaker;
        private readonly OwnerFaker _ownerFaker;
        private readonly ChickenCoopFaker _chickenCoopFaker;
        private readonly ChickenFaker _chickenFaker;

        public EntityFakes(
            AddressFaker addressFaker, 
            ChickenBreedFaker chickenBreedFaker, 
            OwnerFaker ownerFaker, 
            ChickenCoopFaker chickenCoopFaker,
            ChickenFaker chickenFaker)
        {
            ConfigureAutoFaker();
            _addressFaker = addressFaker;
            _chickenBreedFaker = chickenBreedFaker;
            _ownerFaker = ownerFaker;
            _chickenCoopFaker = chickenCoopFaker;
            _chickenFaker = chickenFaker;
            MapCollections();
        }

        private void MapCollections()
        {
            Addresses = _addressFaker.Entities;
            ChickenBreeds = _chickenBreedFaker.Entities;
            Owners = _ownerFaker.Entities;
            ChickenCoops = _chickenCoopFaker.Entities;
            Chickens = _chickenFaker.Entities;
        }

        private void ConfigureAutoFaker()
        {
            Bogus.DataSets.Date.SystemClock = () =>
                DateTime.SpecifyKind(new DateTime(2020, 1, 8, 12, 00, 00), DateTimeKind.Utc);

            AutoFaker.Configure(builder =>
            {
                builder
                    .WithRecursiveDepth(1)
                    .WithOverride(new IdsByNameOverride())
                    .WithOverride(new AddressOverride())
                    .WithConventions(AddressConventions.StreetAddressConvention)
                    .WithConventions(AddressConventions.ZipConvention)
                    .WithConventions(PriceConventions.PriceConvention)
                    ;
            });
        }


        public void GenerateAll()
        {
            _addressFaker.Generate(10);
            _chickenBreedFaker.Generate(200);
            _ownerFaker.Generate(20);
            _chickenCoopFaker.Generate(20);
            _chickenFaker.Generate(200);
        }
    }
}
