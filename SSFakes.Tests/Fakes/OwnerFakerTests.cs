using FluentAssertions;
using FluentAssertions.Execution;
using PoultryCloningFactory.Fakes;
using Xunit;

namespace SSFakes.Tests.Fakes
{
    public class OwnerFakerTests
    {
        [Fact]
        public void Generate_WithAddressFakerInjected_GeneratesOwnerWithAddress()
        {
            var addressFaker = new AddressFaker();

            var sut = new OwnerFaker(5, addressFaker);

            var actual = sut.Generate();

            using (new AssertionScope()) { 
                actual.Name.Should().NotBeEmpty();
                actual.Address.City.Should().NotBeEmpty();
            }
        }
    }
}
