using AutoBogus.Conventions;

namespace PoultryCloningFactory.ModelAutoGeneratorConventions
{
    public static class PriceConventions
    {
        public static void PriceConvention(AutoConventionConfig config)
        {
            config.Price.Aliases("AdoptionFee");
        }
    }
}
