using Api.Controllers.Models;
using FluentAssertions;
using Infrastructure.SqlServer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace Test.Integ
{
    public class WebApplicationTest
    {
        private readonly HttpClient _client;

        public WebApplicationTest()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => builder.ConfigureTestServices(
                   services =>
                   {
                       services.RemoveAll(typeof(DbContextOptions<ExpenseContext>));
                       services.AddDbContext<ExpenseContext>(options =>
                          options.UseInMemoryDatabase("TestDb"));
                   }));

            _client = application.CreateClient();
        }

        [Fact]
        public async Task IntegrationTest()
        {
            //arrange
            var userId = 1;
            var expenseToCreate = new ExpenseToCreate
            {
                Amount = 16.4m,
                Comment = "The comment",
                Currency = "USD",
                Date = DateTime.Now.Date,
                Nature = Nature.Restaurant
            };
            var httpContent = PrepareHttpContent(expenseToCreate);

            //act
            var creationResult = await _client.PostAsync($"users/{userId}/expenses", httpContent).ConfigureAwait(false);
            var expenseListResult = await _client.GetAsync($"users/{userId}/expenses").ConfigureAwait(false);

            //assert
            var jsonContent = await expenseListResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<ExpenseResultItems[]>(jsonContent, jsonSerializerOptions);

            var expected = new[]
            {
                new ExpenseResultItems
                {
                    Amount = 16.4m,
                    Comment = "The comment",
                    Currency = "USD",
                    Date = DateTime.Now.Date,
                    Nature = "Restaurant",
                    User = "Anthony Stark"
                }
            };

            result.Should().BeEquivalentTo(expected);
        }

        private StringContent PrepareHttpContent(ExpenseToCreate expenseToCreate)
        {
            var json = JsonSerializer.Serialize(expenseToCreate, jsonSerializerOptions);
            return new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
        }

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
