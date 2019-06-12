using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Infrastructure.SQL.Database;

namespace SecurityWeakness.Infrastructure.SQL.Repositories
{
    internal class ProductRepositoryBase : CrudRepository<Product, int>
    {
        private readonly ProductDbContext context;
        private readonly string CommentTableName;

        public ProductRepositoryBase(ProductDbContext context) : base(context)
        {
            this.context = context;
            CommentTableName = GetTableName<Comment>(context);
        }

        public void AddComment(int productId, Comment comment)
        {
            var pu = Get(productId);
            pu.Comments = pu.Comments ?? new List<Comment>();
            pu.Comments.Add(comment);
            context.SaveChanges();
        }

        public Product Update(Product product)
        {
            var pu = Get(product.Id);

            pu.Name = product.Name;
            pu.Price = product.Price;
            pu.Sku = product.Sku;
            pu.Description = product.Description;
            pu.Comments = product.Comments;

            context.SaveChanges();
            return pu;
        }

        protected IEnumerable<Comment> GetComments(int productId)
        {
            var sql = $"SELECT * FROM {CommentTableName} WHERE product_id={productId}";
            return context.Comments.FromSql(sql).ToList();
        }

        protected string GetTableName<TEntity>(ProductDbContext context)
        {
            return context.Model.FindEntityType(typeof(TEntity)).Relational().TableName;
        }
    }
}