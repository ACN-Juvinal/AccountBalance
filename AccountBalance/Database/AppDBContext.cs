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
            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                    UserName = "testuser",
                    NormalizedUserName = "testuser",
                    Email = "test@gmail.com",
                    NormalizedEmail = "test@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "SOME_ADMIN_PLAIN_PASSWORD"),
                    SecurityStamp = string.Empty
                });
            builder.Entity<UserBalance>().HasData(
                new UserBalance
                {
                    Id = 1,
                    Amount = 500M,
                    DateCreated = DateTimeOffset.Now.AddDays(-7),
                    Status = "Active"
                }
                );
            builder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    Amount = 10M,
                    Date = DateTimeOffset.Now.AddDays(-5),
                    Status = "Closed",
                    Reason = "Paid through credit card"
                },
                new Payment
                {
                    Id = 2,
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
