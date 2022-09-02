using FluentAssertions;
using PoultryCloningFactory.Fakes;
using Xunit;
using Xunit.Abstractions;

namespace SSFakes.Tests.Fakes
{
    public class AddressFakerTests
    {
        private readonly ITestOutputHelper _output;

        public AddressFakerTests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void Generate_WithoutSeed_GeneratesCity()
        {
            var sut = new AddressFaker();

            var actual = sut.Generate();

            _output.WriteLine(actual.City);
            actual.City.Should().NotBeEmpty();
        }

        [Fact]
        public void Generate_WithSeed_GeneratesConsistentData()
        {
            var sut = new AddressFaker();

            var actual = sut.UseSeed(1234).Generate();

            _output.WriteLine(actual.City);

            // You _can_ do this, but there is really no guarantee the library won't change the seed data.
            actual.City.Should().BeEquivalentTo("Connecticut");
        }
    }
}
