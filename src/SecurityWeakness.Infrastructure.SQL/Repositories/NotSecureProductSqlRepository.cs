using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Infrastructure.SQL.Database;

namespace SecurityWeakness.Infrastructure.SQL.Repositories
{
    internal class NotSecureProductSqlRepository : INotSecureProductSqlRepository
    {
        private readonly ProductDbContext context;
        private readonly string TableName;

        public NotSecureProductSqlRepository(ProductDbContext context)
        {
            this.context = context;
            var relational = context.Model.FindEntityType(typeof(Product)).Relational();
            TableName = relational.TableName;
        }

        public IEnumerable<Product> Get()
        {
            var sql = string.Format($"SELECT * FROM {TableName}");
            return context.Products.FromSql(sql).ToList();
        }

        public Product GetSingleBySku(string sku)
        {
            var sql = $"SELECT * FROM {TableName} WHERE sku='{sku}' LIMIT 1";
            return context.Products.FromSql(sql).ToArray().Single();
        }
    }
}
