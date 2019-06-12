using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Infrastructure.SQL.Database;

namespace SecurityWeakness.Infrastructure.SQL.Repositories
{
    internal class NotSecureProductRepository : ProductRepositoryBase, INotSecureProductRepository
    {
        private readonly ProductDbContext context;
        private readonly string ProductTableName;

        public NotSecureProductRepository(ProductDbContext context) : base(context)
        {
            this.context = context;
            ProductTableName = GetTableName<Product>(context);
        }

        public new IEnumerable<Product> Get()
        {
            var sql = string.Format($"SELECT * FROM {ProductTableName}");
            return context.Products.FromSql(sql).ToList();
        }

        public Product GetSingleBySku(string sku)
        {
            var sql = $"SELECT * FROM {ProductTableName} WHERE sku='{sku}' LIMIT 1";
            var product = context.Products.FromSql(sql).ToArray().Single();
            product.Comments = GetComments(product.Id).ToArray();

            return product;
        }
    }
}
