using System.Collections.Generic;
using SecurityWeakness.Domain.Entities;

namespace SecurityWeakness.Domain.Extensibility.Repositories
{
    public interface IProductSqlRepository
    {
        IEnumerable<Product> Get();

        Product GetSingleBySku(string sku);
    }

    public interface INotSecureProductSqlRepository : IProductSqlRepository
    {
    }

    public interface ISecureProductSqlRepository : IProductSqlRepository
    {
    }
}
