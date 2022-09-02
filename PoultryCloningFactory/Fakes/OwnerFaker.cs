using AutoBogus;
using Bogus;
using PoultryPopulation.Entities;

namespace PoultryCloningFactory.Fakes
{
    public class OwnerFaker : LookupEntityBaseFaker<Owner>
    {
        public OwnerFaker(int minimumSet, AddressFaker addressFaker) : base(minimumSet)
        {
            base.RuleFor(a => a.AddressId, () => addressFaker.Generate().Id);
        }
    }
}