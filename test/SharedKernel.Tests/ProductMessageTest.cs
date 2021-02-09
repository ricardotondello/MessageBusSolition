using FluentAssertions;
using Xunit;

namespace SharedKernel.Tests
{
    public class ProductMessageTest
    {
        [Fact]
        public void Product_Message_should_not_be_null()
        {
            var p = new ProductMessage {Name = "bla"};

            p.Should().NotBeNull();
            p.Name.Should().NotBeNull();
        } 
    }
}