using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using SharedKernel;
using Xunit;
using Xunit.Abstractions;

namespace MyStoreControl.IntegrationTests
{
    public class ProductIntegrationTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ProductIntegrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Get_Returns_success()
        {
            //Arrange
            
            //Act
            var response = TestClient.GetAsync("/Product");
            
            //Assert

            response.Result.StatusCode.Should().Be(StatusCodes.Status200OK);

            var items = ((await response.Result.Content.ReadFromJsonAsync<IEnumerable<Product>>()) ?? Array.Empty<Product>()).ToList();
            
            items.Should().NotBeNull();
            items.Should().HaveCountGreaterThan(0);
            
        }
    }
}