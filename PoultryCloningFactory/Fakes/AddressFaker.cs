using Bogus.Extensions;
using PoultryPopulation.Entities;

namespace PoultryCloningFactory.Fakes
{
    public sealed class AddressFaker : EntityBaseFaker<Address>
    {
        public AddressFaker()
        {
            RuleFor(a => a.StreetAddress2, f => f.Address.StreetAddress().OrNull(f, .7f));
            RuleFor(a => a.State, f => f.Address.StateAbbr());
        }
    }
}
