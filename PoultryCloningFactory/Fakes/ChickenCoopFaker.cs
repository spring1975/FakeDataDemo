using PoultryPopulation.Entities;

namespace PoultryCloningFactory.Fakes
{
    public sealed class ChickenCoopFaker : LookupEntityBaseFaker<ChickenCoop>
    {
        public ChickenCoopFaker(int minimumSet, OwnerFaker ownerFaker) : base(minimumSet)
        {
            RuleFor(a => a.OwnerId, f => f.PickRandom(ownerFaker.Entities).Id);

            // NOTE: Alternative to WithSkip???
            RuleFor(c => c.HousedChickens, () => null);
        }
    }
}
