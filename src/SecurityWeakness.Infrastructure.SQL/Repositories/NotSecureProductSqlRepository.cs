using System;
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
        private readonly string ProductTableName;
        private readonly string CommentTableName;

        public NotSecureProductSqlRepository(ProductDbContext context)
        {
            this.context = context;
            ProductTableName = GetTableName<Product>(context);
            CommentTableName = GetTableName<Comment>(context);
        }

        public IEnumerable<Product> Get()
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

        private IEnumerable<Comment> GetComments(int productId)
        {
            var sql = $"SELECT * FROM {CommentTableName} WHERE product_id={productId}";
            return context.Comments.FromSql(sql).ToList();
        }

        private string GetTableName<TEntity>(ProductDbContext context)
        {
            return context.Model.FindEntityType(typeof(TEntity)).Relational().TableName;
        }
    }
}
