using AccountBalance.Features;
using AccountBalance.Tests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using static AccountBalance.Features.GetBalanceandPayments;

namespace AccountBalance.Tests
{
    public class AccountBalanceTests
    {
        private readonly HttpClient httpClient;
        private string _token;

        [Fact]
        public void Test_GetUserBalanceandPayments()
        {
            var UserId = "6f41830b-2938-4d61-b3fd-35c5dac80f77";
            var balance = MockPaymentsRepository.Balances.Where(s => s.UserId == UserId).Sum(s => s.Amount);
            var payments = MockPaymentsRepository.Payments.Where(s => s.UserId == UserId).OrderByDescending(s => s.Date);

            Assert.Equal(2, payments.Count());
        }
    }
}
