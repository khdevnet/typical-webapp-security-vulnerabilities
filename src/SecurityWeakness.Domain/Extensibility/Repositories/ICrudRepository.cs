using System.Collections.Generic;
using SecurityWeakness.Domain.Entities;

namespace SecurityWeakness.Domain.Extensibility.Repositories
{
    public interface ICrudRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        TEntity Get(TId id);

        IEnumerable<TEntity> Get();

        TEntity Add(TEntity product);

        TId Delete(TId id);
    }
}