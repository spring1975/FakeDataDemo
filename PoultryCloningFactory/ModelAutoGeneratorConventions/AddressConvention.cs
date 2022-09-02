using AutoBogus.Conventions;

namespace PoultryCloningFactory.ModelAutoGeneratorConventions
{
    public static class AddressConventions
    {
        public static void StreetAddressConvention(AutoConventionConfig config)
        {
            config.StreetAddress.Aliases("StreetAddress1");
            config.SecondaryAddress.Aliases("StreetAddress2");
        }

        public static void ZipConvention(AutoConventionConfig config)
        {
            config.ZipCode.Aliases("Zip", "PostalCode");
        }
    }
}
