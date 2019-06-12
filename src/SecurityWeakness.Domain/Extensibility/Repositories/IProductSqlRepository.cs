namespace SecurityWeakness.Domain.Extensibility.Repositories
{
    public interface INotSecureProductRepository : IProductRepository
    {
    }

    public interface ISecureProductRepository : IProductRepository
    {
    }
}
