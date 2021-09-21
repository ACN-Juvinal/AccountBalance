using AccountBalance.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountBalance.Database
{
    public class AppDBContext : IdentityDbContext<IdentityUser>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var defaultUser = new IdentityUser
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
            };
            builder.Entity<IdentityUser>().HasData(defaultUser);
            builder.Entity<UserBalance>().HasData(
                new UserBalance
                {
                    Id = 1,
                    UserId = defaultUser.Id,
                    Amount = 500M,
                    DateCreated = DateTimeOffset.Now.AddDays(-7),
                    Status = "Active"
                }
                );
            builder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    UserId = defaultUser.Id,
                    Amount = 10M,
                    Date = DateTimeOffset.Now.AddDays(-5),
                    Status = "Closed",
                    Reason = "Paid through credit card"
                },
                new Payment
                {
                    Id = 2,
                    UserId = defaultUser.Id,
                    Amount = 10M,
                    Date = DateTimeOffset.Now,
                    Status = "Open"
                }
                );

            base.OnModelCreating(builder);
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }
    }
}
