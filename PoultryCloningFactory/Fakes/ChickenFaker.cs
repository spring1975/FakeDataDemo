using Bogus.DataSets;
using PoultryPopulation.Entities;

namespace PoultryCloningFactory.Fakes
{
    public sealed class ChickenFaker : EntityBaseFaker<Chicken>
    {
        public ChickenFaker(OwnerFaker ownerFaker, ChickenBreedFaker chickenBreedFaker, ChickenCoopFaker chickenCoopFaker)
        {
            RuleFor(c => c.Gender, f => f.PickRandom<Gender>());
            RuleFor(c => c.Name, (f, c) => f.Name.FirstName(c.Gender == Gender.Male ? Name.Gender.Male : Name.Gender.Female));
            RuleFor(c => c.OwnerId, f => f.PickRandom(ownerFaker.Entities).Id);
            RuleFor(c => c.ChickenBreedId, f => f.PickRandom(chickenBreedFaker.Entities).Id);
            RuleFor(c => c.ChickenCoopId, f => f.PickRandom(chickenCoopFaker.Entities).Id);
        }
    }
}
