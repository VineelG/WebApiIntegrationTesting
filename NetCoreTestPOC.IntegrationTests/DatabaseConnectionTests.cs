using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NorthwindSqlite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestingPOC.IntegrationTests
{
    public class SqliteDatabaseTests : IClassFixture<ApiWebApplicationFactory<Program>> // Replace with your startup class
    {
        private readonly ApiWebApplicationFactory<Program> _factory;
        private HttpClient _client;

        public SqliteDatabaseTests(ApiWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task CanConnectToDatabase()
        {
            // Arrange
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NorthwindSqliteContext>(); 

            // Act
            var canConnect = await dbContext.Database.CanConnectAsync();

            // Assert
            Assert.True(canConnect);
        }
    }

}




