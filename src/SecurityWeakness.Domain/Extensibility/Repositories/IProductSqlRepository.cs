using System.Collections.Generic;
using SecurityWeakness.Domain.Entities;

namespace SecurityWeakness.Domain.Extensibility.Repositories
{
    public interface IProductSqlRepository
    {
        IEnumerable<Product> Get();

        IEnumerable<Product> GetByName(string name);
    }
}
