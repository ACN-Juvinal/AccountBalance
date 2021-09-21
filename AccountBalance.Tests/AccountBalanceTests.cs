using AccountBalance.Features;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AccountBalance.Tests
{
    public class AccountBalanceTests
    {
        private readonly HttpClient httpClient;
        private string _token;

        [Fact]
        public async Task Test_GetUserBalanceandPayments()
        {
            // Act
            var response = await httpClient.GetAsync("api/getbalanceandpayments");

            // Assert
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var userBalance = JsonSerializer.Deserialize<GetBalanceandPayments.Response>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //Assert.Equal(3, userBalance.Payments);
        }
    }
}
