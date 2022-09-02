using System;
using System.Linq;
using AutoBogus;

namespace PoultryCloningFactory.ModelAutoGeneratorOverrides
{
    public class IdsByNameOverride : AutoGeneratorOverride
    {
        public override bool CanOverride(AutoGenerateContext context)
        {
            string[] ids = {"Id", "OwnerId"};
            
            return context.GenerateType == typeof(int) && ids.Contains(context.GenerateName, StringComparer.InvariantCultureIgnoreCase);
        }

        public override void Generate(AutoGenerateOverrideContext context)
        {
            context.Instance = context.Faker.Random.Int(1, 2000);
        }
    }
}