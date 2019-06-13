using System.Collections.Generic;
using SecurityWeakness.Domain.Entities;

namespace SecurityWeakness.Domain.Extensibility.Services
{
    public interface IProductService
    {
        Product Add(Product product);

        Product Update(Product product);

        void Delete(Product product);

        Product GetSingleBySku(string sku);

        void AddComment(int productId, Comment comment);

        void DeleteComment(int productId, int commentId);

        IEnumerable<Product> Get();
    }

    public interface INotSecureProductService : IProductService
    {
    }

    public interface ISecureProductService : IProductService
    {
    }
}
