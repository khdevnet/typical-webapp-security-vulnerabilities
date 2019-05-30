using Autofac;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Infrastructure.SQL.Repositories;

namespace SecurityWeakness.Infrastructure.SQL
{
    public class SqlAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CrudRepository<Product, int>>().As<ICrudRepository<Product, int>>();
        }
    }
}