using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NorthwindSqlite.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingPOC.IntegrationTests
{
    public class ApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                //var serviceProvider = new ServiceCollection()
                //    .AddEntityFrameworkSqlite()
                //    .BuildServiceProvider();

                services.AddSingleton<DbConnection>(container =>
                {
                    // Specify physical SQLite file path
                    var connection = new SqliteConnection("Data Source=northwind.sqlite");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<NorthwindSqliteContext>((container,options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                    
                    //options.UseInternalServiceProvider(serviceProvider);
                });

            });
        }

    }    
    

}
