using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Infrastructure.SQL.Database;

namespace SecurityWeakness.Web.Configurations
{
    public static class DatabaseConfigurationExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContext<ProductDbContext>(
                options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(ProductDbContext).GetTypeInfo().Assembly.GetName().Name)));
        }

        public static void ApplyDbMigrations(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                ProductDbContext context = serviceScope.ServiceProvider.GetRequiredService<ProductDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();

                AddProducts(context.Products);

                context.SaveChanges();
            }
        }

        private static void AddProducts(DbSet<Product> productSet)
        {
            var products = new List<Product>
            {
                new Product {Sku = "p1", Name = "R2-D2", Price = 200, Comments = GetComments()},
                new Product {Sku = "p2", Name = "Speeder", Price = 300},
                new Product {Sku = "p3", Name = "Speeder2", Price = 500},
                new Product {Sku = "p4", Name = "Speeder3", Price = 600},
                new Product {Sku = "p5", Name = "BB-8", Price = 400},
                new Product {Sku = "p6", Name = "Blaster", Price = 700},
                new Product {Sku = "p7", Name = "Death star", Price = 8000}
            };

            productSet.AddRange(products);
        }

        private static List<Comment> GetComments()
        {
            return new List<Comment>
            {
                new Comment{ Text = "he is a best friend!!!", UserEmail = "luke.skywalker@rebels.com" },
                new Comment{ Text = "he is pain in the ass, but I like it.", UserEmail = "dart.veider@deathstar.com" },
            };
        }
    }
}