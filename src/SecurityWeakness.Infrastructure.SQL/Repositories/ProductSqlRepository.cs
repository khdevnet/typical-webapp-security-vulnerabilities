using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Infrastructure.SQL.Database;

namespace SecurityWeakness.Infrastructure.SQL.Repositories
{
    internal class ProductSqlRepository : IProductSqlRepository
    {
        private readonly ProductDbContext context;
        private readonly string TableName;

        public ProductSqlRepository(ProductDbContext context)
        {
            this.context = context;
            var relational = context.Model.FindEntityType(typeof(Product)).Relational();
            TableName = relational.TableName;
        }

        public IEnumerable<Product> Get()
        {
            return context.Products.FromSql(string.Format("SELECT * FROM {0}", TableName)).ToList();
        }

        public IEnumerable<Product> GetByName(string name)
        {
            return context.Products.FromSql(string.Format("SELECT * FROM {0} WHERE name={1}", TableName, name)).ToList();
        }
    }
}
