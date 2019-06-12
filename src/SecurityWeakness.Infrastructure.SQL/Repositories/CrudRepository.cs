using System.Collections.Generic;
using System.Linq;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Infrastructure.SQL.Database;

namespace SecurityWeakness.Infrastructure.SQL.Repositories
{
    internal class CrudRepository<TEntity, TId> : ICrudRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly ProductDbContext context;

        public CrudRepository(ProductDbContext context)
        {
            this.context = context;
        }

        public TEntity Add(TEntity entity)
        {
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public TId Delete(TId id)
        {
            context.Remove(Get(id));
            context.SaveChanges();
            return id;
        }

        public TEntity Get(TId id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return context.Set<TEntity>().ToList();
        }
    }
}