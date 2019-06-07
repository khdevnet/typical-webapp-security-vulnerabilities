using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityWeakness.Domain.Entities;

namespace SecurityWeakness.Infrastructure.SQL.Database
{
    public class ProductDbContext : IdentityDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplySnakeCaseNamingConventions(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Comment>()
                .HasKey(x => x.Id);

            SeedUsers(modelBuilder);
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            var users = new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = "0bb4e8c9-a044-4340-a052-ce9eb50ed1b5",
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    EmailConfirmed = false,
                    PasswordHash ="AQAAAAEAACcQAAAAEC63d4qwalNk5geOuxz/wlElCED7jk1I/D0J9xm1Lci++VIJrUy89DDmf4qx69CdRw==",
                    SecurityStamp = "VTFL6GT56KFZCCX2XI6SMNUIQU6IGUV4",
                    ConcurrencyStamp = "e35570ee-8866-45d5-9106-8b59c5101eba",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            };

            users.ForEach(p => modelBuilder.Entity<IdentityUser>().HasData(p));
        }

        private static void ApplySnakeCaseNamingConventions(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = ToSnakeCase(entityType.Relational().TableName.Singularize());
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    property.Relational().ColumnName = ToSnakeCase(property.Relational().ColumnName);
                }
            }
        }

        public static string ToSnakeCase(string str)
        {
            return string.Concat(str.Select((symbol, i) => i > 0 && char.IsUpper(symbol) ? $"_{symbol}" : $"{symbol}")).ToLower();
        }
    }
}