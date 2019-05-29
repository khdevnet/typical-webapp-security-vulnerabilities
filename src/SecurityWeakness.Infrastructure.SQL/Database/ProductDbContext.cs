using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecurityWeakness.Domain.Entities;

namespace SecurityWeakness.Infrastructure.SQL.Database
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.BuildProduct();

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "R2-D2", Price=200 },
                new Product { Id = 2, Name = "Speeder", Price=300 },
                new Product { Id = 3, Name = "BB-8" , Price=400},
                new Product { Id = 4, Name = "Blaster" , Price=700},
                new Product { Id = 5, Name = "Death star" , Price=8000}
            };

            products.ForEach(p => modelBuilder.Entity<Product>().HasData(p));
        }
    }
}