using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Infrastructure.SQL.Database;

namespace SecurityWeakness.Infrastructure.SQL.Repositories
{
    internal class SecureProductRepository : ProductRepositoryBase, ISecureProductRepository
    {
        private readonly ProductDbContext context;
        private readonly string TableName;

        public SecureProductRepository(ProductDbContext context) : base(context)
        {
            this.context = context;
            var relational = context.Model.FindEntityType(typeof(Product)).Relational();
            TableName = relational.TableName;
        }

        public new IEnumerable<Product> Get()
        {
            var sql = string.Format($"SELECT * FROM {TableName}");
            return context.Products.FromSql(sql).ToList();
        }

        public Product GetSingleBySku(string sku)
        {
            var product = context.Products.FromSql($"SELECT * FROM {TableName} WHERE sku={{0}} LIMIT 1", sku).ToArray().Single();
            product.Comments = GetComments(product.Id).ToArray();
            return product;
        }
    }
}
