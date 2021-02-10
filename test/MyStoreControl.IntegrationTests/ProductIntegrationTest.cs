using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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
        public async Task Get_returns_success()
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

        [Fact]
        public async Task Post_resturns_success()
        {
            //Arrange
            var productMessage = new ProductMessage
            {
                Name = "Test"
            };
            
            //Act
            var response = await TestClient.PostAsync("/Product", new StringContent(
                JsonConvert.SerializeObject(productMessage), 
                Encoding.UTF8, 
                "application/json"));
            
            //Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            await Task.FromResult(true);
        } 
    }
}