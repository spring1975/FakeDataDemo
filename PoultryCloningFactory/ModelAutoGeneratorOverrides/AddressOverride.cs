using AutoBogus;
using Bogus;
using PoultryPopulation.Entities;

namespace PoultryCloningFactory.ModelAutoGeneratorOverrides
{
    public class AddressOverride : AutoGeneratorOverride
    {
        public override bool CanOverride(AutoGenerateContext context)
        {
            return context.GenerateType == typeof(Address);
        }

        public override void Generate(AutoGenerateOverrideContext context)
        {
            var a = context.Instance as Address;
            Faker f = context.Faker;
            
            // ReSharper disable once PossibleNullReferenceException
            a.State = f.Address.StateAbbr();
        }
    }
}
