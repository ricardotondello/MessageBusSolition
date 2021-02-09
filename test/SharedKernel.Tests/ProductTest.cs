using System;
using FluentAssertions;
using Xunit;

namespace SharedKernel.Tests
{
    public class ProductTest
    {
        [Fact]
        public void Product_not_should_be_null()
        {
            var p = new Product { Id = Guid.NewGuid(), Name = "aa", Price = 99.99, Stock = 88};

            p.Should().NotBeNull();
            p.Id.Should().NotBeEmpty();
            p.Name.Should().NotBeEmpty();
            p.Price.Should().Be(99.99);
            p.Stock.Should().Be(88);
        }
    }
}