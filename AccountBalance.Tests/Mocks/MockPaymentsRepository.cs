using AccountBalance.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBalance.Tests.Mocks
{
    public class MockPaymentsRepository
    {
        public static List<Payment> Payments { get; } = new List<Payment>
        {
            new Payment
                {
                    Id = 1,
                    UserId = "6f41830b-2938-4d61-b3fd-35c5dac80f77",
                    Amount = 10M,
                    Date = DateTimeOffset.Now.AddDays(-5),
                    Status = "Closed",
                    Reason = "Paid through credit card"
                },
            new Payment
            {
                Id = 2,
                UserId = "6f41830b-2938-4d61-b3fd-35c5dac80f77",
                Amount = 10M,
                Date = DateTimeOffset.Now,
                Status = "Open"
            }
        };

        public static List<UserBalance> Balances { get; } = new List<UserBalance>
        {
            new UserBalance
            {
                Id = 1,
                UserId = "6f41830b-2938-4d61-b3fd-35c5dac80f77",
                Amount = 500M,
                DateCreated = DateTimeOffset.Now.AddDays(-7),
                Status = "Active"
            }
        };

        public static List<IdentityUser> Users { get; } = new List<IdentityUser>
        {
            new IdentityUser
            {
                Id = "6f41830b-2938-4d61-b3fd-35c5dac80f77",
                UserName = "testuser",
                NormalizedUserName = "testuser",
                Email = "test@gmail.com",
                NormalizedEmail = "test@gmail.com",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEMkmBw0P35G0eOfg0kGGcKxRZHIYkcPj/Fo3TCa8s/Sj38DeAmLdJ07TD5EuhYS4vA==",
                SecurityStamp = "FBLTOOQQ7GUHQCJDVU5EZUINTZYAYHUU",
                ConcurrencyStamp = "8cb056f2-9942-421b-9af1-ffd3a70a82d9"
            }
        };
    }
}
