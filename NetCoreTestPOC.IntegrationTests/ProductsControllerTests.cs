using Azure.Core;
using IntegrationTestingPOC.IntegrationTests;
using NetCoreAPI_IntegrationTesting;
using Newtonsoft.Json;
using NorthwindSqlite.Models;
using System.Net;
using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using FluentAssertions;

namespace IntegrationTestingPOC.IntegrationTests
{
    [TestCaseOrderer("IntegrationTestingPOC.IntegrationTests.TestCaseOrderer", "IntegrationTestingPOC.IntegrationTests")]
    public class ProductsControllerTests : IClassFixture<ApiWebApplicationFactory<Program>>
    {
        private readonly ApiWebApplicationFactory<Program> _factory;
        private HttpClient _client;

        public ProductsControllerTests(ApiWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task T1_Post_ProductRequest_AddsNewProduct()
        {
            var productToInsert = new Product()
            {
                Id = 80,
                ProductName = "NewTestProduct",
                SupplierId = 1,
                CategoryId = 1,
                QuantityPerUnit = "10 boxes x 20 bags",
                UnitPrice = 10,
                UnitsInStock = 39,
                UnitsOnOrder = 0,
                ReorderLevel = 10,
                Discontinued = 0,
                Category = null,
                OrderDetails = new List<OrderDetail>(),
                Supplier = null
            };
            var productContent = JsonConvert.SerializeObject(productToInsert);
            HttpContent content = new StringContent(productContent, Encoding.UTF8, "application/json");
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/api/Products", content);
            response.EnsureSuccessStatusCode();


        }

        [Fact]
        public async Task T2_GetProducts_GetsProductList()
        {                
            var response = await _client.GetAsync("/api/Products");
            response.EnsureSuccessStatusCode(); // Assert the response is 200 OK
            var jsonString = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(jsonString);
            products.Should().HaveCount(80);
        }


        [Fact]
        public async Task T3_PutProduct_UpdatesProduct()
        {
            var productToUpdate = new Product()
            {
                Id = 80,
                ProductName = "UpdatedTestProduct",
                UnitPrice = 20,
                UnitsInStock = 100,   
                SupplierId = 1,
                CategoryId = 1,
                QuantityPerUnit = "10 boxes x 20 bags",
                UnitsOnOrder = 0,
                ReorderLevel = 10,
                Discontinued = 0,
                Category = null,
                OrderDetails = new List<OrderDetail>(),
                Supplier = null
            };
            var productContent = JsonConvert.SerializeObject(productToUpdate);
            HttpContent content = new StringContent(productContent, Encoding.UTF8, "application/json");
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var productIdToDelete = 80;
            var response = await _client.PutAsync($"/api/Products/{productIdToDelete}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task T4_GetProduct_GetsUpdatedProductDetails()
        {
            var productIdToGet = 80;
            var response = await _client.GetAsync($"/api/Products/{productIdToGet}");
            response.EnsureSuccessStatusCode(); // Assert the response is 200 OK
            //var jsonString = await response.Content.ReadAsStringAsync();
            var productResponse = await response.Content.ReadFromJsonAsync<Product>();
            productResponse?.Id.Should().BePositive();
            productResponse?.ProductName.Should().Be("UpdatedTestProduct");
            productResponse?.UnitPrice.Should().Be(20);

        }

        [Fact]
        public async Task T5_DeleteProduct_ReturnsSuccessResponse()
        {
            // Arrange
            var productIdToDelete = 80; 

            // Act
            var response = await _client.DeleteAsync($"/api/Products/{productIdToDelete}");

            // Assert
            response.EnsureSuccessStatusCode();
        }


    }
}