using System;
using System.Diagnostics;
using AutoBogus;
using AutoBogus.Conventions;
using Bogus;
using PoultryCloningFactory;
using PoultryCloningFactory.ModelAutoGeneratorOverrides;
using PoultryCloningFactory.ModelAutoGeneratorConventions;
using PoultryPopulation.Entities;
using PoultryPopulation.Entities.Abstracts;
using Utilities;
using Xunit;
using Xunit.Abstractions;

namespace SSFakes.Tests
{
    public class FakeGenerators
    {
        private readonly ITestOutputHelper _output;

        public FakeGenerators(ITestOutputHelper output)
        {
            _output = output;
            #region 
            Bogus.DataSets.Date.SystemClock = () => DateTime.Parse("8/8/2019 2:00 PM");
            #endregion
        }

        /// <summary>
        /// Simple fake setup for a POCO object with a default constructor
        /// </summary>
        [Fact]
        public void Generate_PocoChicken_FakesAChicken()
        {
            Faker<Chicken> chickenFaker = new Faker<Chicken>()
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.Birthdate, f => f.Date.Past(4))
                .RuleFor(c => c.Id, f => f.IndexFaker);

            var chickens = chickenFaker.Generate();

            _output.WriteLine(PayloadJsonSerializationHelpers.ToJSON(chickens));
        }
        
        /// <summary>
        /// Simple fake setup for a POCO having a greedy
        /// constructor while also filling in settable properties
        ///
        /// Illustrates use of Rules for reuse
        /// Illustrates use of SystemClock
        /// </summary>
        [Fact]
        public void Generate_ChickenBreed_FakesAChickenBreed()
        {

            Faker<ChickenBreed> chickenBreedFaker = new Faker<ChickenBreed>()
                .CustomInstantiator(f =>
                    new ChickenBreed(
                        name: f.Name.JobDescriptor(),
                        primaryColor: f.PickRandom<Color>()
                    )
                )
                .RuleFor(cb => cb.Id, f => f.IndexFaker)
                .Rules(EntityBaseRules)
                ;

            var chickenBreed = chickenBreedFaker.Generate();

            _output.WriteLine(PayloadJsonSerializationHelpers.ToJSON(chickenBreed));
        }

        private static void EntityBaseRules<T>(Faker f, T obj) where T : EntityBase
        {
            // Base properties
            obj.Created = f.Date.Past();
            obj.CreatedBy = f.Name.FullName();
            
            obj.Modified = Bogus.DataSets.Date.SystemClock();
            obj.ModifiedBy = f.Name.FullName();
        }

        /// <summary>
        /// 
        /// Illustrates properties accessing each other
        /// Illustrates last defined rule wins
        /// </summary>
        [Fact]
        public void Generate_GreedyConstructorChicken_FakesAChicken()
        {
            Faker<Chicken> chickenFaker = new Faker<Chicken>()
                .CustomInstantiator(f =>
                {
                    var isAdoptable = f.Random.Bool(3f);
                    return new Chicken(
                        name: f.Name.FirstName(),
                        gender: f.PickRandom<Gender>(),
                        birthdate: f.Date.Past(4),
                        isAdoptable: isAdoptable,
                        adoptionFee: isAdoptable ? f.Finance.Amount(5, 100) : (decimal?) null,
                        chickenBreedId: f.Random.Number(1, 200),
                        chickenBreed: null,
                        chickenCoopId: f.Random.Number(1, 200),
                        chickenCoop: null,
                        ownerId: f.Random.Number(1, 200)
                    );
                })
                .Rules(EntityBaseRules)
                // Setting a value relative to another property
                .RuleFor(c => c.Modified, (f, c) => f.Date.Between(c.Created.Value.DateTime, Bogus.DataSets.Date.SystemClock()))
                ;

            var chickens = chickenFaker.Generate();

            _output.WriteLine(PayloadJsonSerializationHelpers.ToJSON(chickens));
        }
        
        /// <summary>
        /// Illustrates generating virtual member data
        /// Illustrates Consistent output (Determinism) with .UseSeed()
        /// </summary>
        [Fact]
        public void Generate_Chicken_FakesAChickenWithOwner()
        {
            Faker<Owner> ownerFaker = new Faker<Owner>()
                .UseSeed(12345)
                .CustomInstantiator(f =>
                    new Owner(f.IndexFaker, f.Name.FirstName(), f.Phone.PhoneNumber()));

            Owner owner = ownerFaker.Generate();
            /* It is tempting to use the same seed number for all instances.
             * However, when using the same seed for all fakers, the 'random'
             * data will start to sync between class instances.
             * Note the names when the seeds are the same for chicken and owner
             */
            Faker<Chicken> chickenFaker = new Faker<Chicken>()
                .UseSeed(12345)
                .CustomInstantiator(f =>
                {
                    var isAdoptable = f.Random.Bool(3f);
                    return new Chicken(
                        name: f.Name.FirstName(),
                        gender: f.PickRandom<Gender>(),
                        birthdate: f.Date.Past(4),
                        isAdoptable: isAdoptable,
                        adoptionFee: isAdoptable ? f.Finance.Amount(5, 100) : (decimal?)null,
                        chickenBreedId: f.Random.Number(1, 200),
                        chickenBreed: null,
                        chickenCoopId: f.Random.Number(1, 200),
                        chickenCoop: null,
                        ownerId: f.Random.Number(1, 200)
                    );
                })
                .RuleFor(c => c.Owner, () => owner);

            var chicken = chickenFaker.Generate();

            _output.WriteLine(PayloadJsonSerializationHelpers.ToJSON(chicken));
        }

        /// <summary>
        /// Introducing AutoBogus
        /// Illustrates WithRecursiveDepth
        /// Illustrates WithSkip
        /// </summary>
        [Fact]
        public void Generate_AutoChicken_FakesTooManyThings()
        {
            var sw = new Stopwatch();
            #region WithRecursiveDepth
            //AutoFaker.Configure(builder => 
            //    builder.WithRecursiveDepth(1));
            #endregion
            #region WithSkip v1
            //AutoFaker.Configure(builder =>
            //{
            //    builder
            //    .WithSkip<Chicken>(cc => cc.ChickenCoop)
            //    .WithSkip<Chicken>(cc => cc.Owner)
            //    .WithSkip<Chicken>(cc => cc.ChickenBreed)
            //    ;
            //});
            #endregion            
            #region WithSkip v2
            //AutoFaker.Configure(builder =>
            //{
            //    builder
            //    .WithSkip<ChickenCoop>(cc => cc.HousedChickens)
            //    .WithSkip<ChickenCoop>(cc => cc.Owner)
            //    .WithSkip<Owner>(o => o.ChickenCoops)
            //    .WithSkip<Owner>(o => o.Chickens)
            //    ;
            //});
            #endregion
            var f = new AutoFaker<Chicken>();

            sw.Start();
            var chickens = f.Generate(10);
            _output.WriteLine($"Generated fakes {sw.Elapsed}");
            _output.WriteLine(PayloadJsonSerializationHelpers.ToJSON(chickens));
        }

        [Fact] 
        public void Generate_AutoChickenUsingOverridesAndConventions_GeneratesBetterFakes()
        {
            var sw = new Stopwatch();
            AutoFaker.Configure(builder =>
            {
                builder
                .WithSkip<ChickenCoop>(cc => cc.HousedChickens)
                .WithSkip<ChickenCoop>(cc => cc.Owner)
                .WithSkip<Owner>(o => o.ChickenCoops)
                .WithSkip<Owner>(o => o.Chickens)
                #region overrides
                .WithOverride(new IdsByNameOverride())
                .WithOverride(new AddressOverride())
                #endregion
                #region conventions
                .WithConventions(AddressConventions.StreetAddressConvention)
                .WithConventions(AddressConventions.ZipConvention)
                .WithConventions(PriceConventions.PriceConvention)
                #endregion
                ;
            });
            var chickenFaker = new AutoFaker<Chicken>()
                .RuleFor(c => c.IsAdoptable, f => f.Random.Bool(.8f))
                .RuleFor(c => c.AdoptionFee, (f, c) => c.IsAdoptable ? f.Finance.Amount(10, 100) : (decimal?)null)
                .UseSeed(123456)
                .Rules(EntityBaseRules);

            sw.Start();
            var chickens = chickenFaker.Generate(10);
            _output.WriteLine($"Generated fakes {sw.Elapsed}");
            
            FakerOutput.ToJSONFile(chickens, "all-poultry", "poultry-census");
        }

    }
}
