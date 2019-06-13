using SecurityWeakness.Domain.Entities;

namespace SecurityWeakness.Domain.Extensibility.Repositories
{
    public interface IProductRepository : ICrudRepository<Product, int>
    {
        Product Update(Product product);

        void AddComment(int productId, Comment comment);

        void DeleteComment(int productId, int commentId);

        Product GetSingleBySku(string sku);
    }
}