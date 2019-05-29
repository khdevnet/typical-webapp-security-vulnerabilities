using System.Collections.Generic;

namespace SecurityWeakness.Domain.Extensibility.Repositories
{
    public interface ICrudRepository<TEntity, TId> where TEntity : class
    {
        TEntity Get(TId id);

        IEnumerable<TEntity> Get();

        TEntity Add(TEntity product);

        TId Delete(TId id);
    }
}