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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplySnakeCaseNamingConventions(modelBuilder);

            SeedUsers(modelBuilder);

            SeedProducts(modelBuilder);
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

        private static void SeedProducts(ModelBuilder modelBuilder)
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Sku = "p1", Name = "R2-D2", Price = 200},
                new Product {Id = 2, Sku = "p2", Name = "Speeder", Price = 300},
                new Product {Id = 3, Sku = "p3", Name = "Speeder2", Price = 500},
                new Product {Id = 4, Sku = "p4", Name = "Speeder3", Price = 600},
                new Product {Id = 5, Sku = "p5", Name = "BB-8", Price = 400},
                new Product {Id = 6, Sku = "p6", Name = "Blaster", Price = 700},
                new Product {Id = 7, Sku = "p7", Name = "Death star", Price = 8000}
            };

            products.ForEach(p => modelBuilder.Entity<Product>().HasData(p));
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