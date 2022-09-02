using PoultryPopulation.Entities;

namespace PoultryCloningFactory.Fakes
{
    public sealed class ChickenBreedFaker : LookupEntityBaseFaker<ChickenBreed>
    {
        // ReSharper disable once EmptyConstructor
        public ChickenBreedFaker(int minimumSet) : base(minimumSet)
        {
            // AutoBogus will take care of all properties
        }
    }
}
