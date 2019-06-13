using System.Collections.Generic;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;

namespace SecurityWeakness.Domain.Services
{
    internal abstract class ProductServiceBase
    {
        private readonly IProductRepository _productRepository;

        public ProductServiceBase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public void AddComment(int productId, Comment comment)
        {
            _productRepository.AddComment(productId, comment);
        }

        public void DeleteComment(int productId, int commentId)
        {
            _productRepository.DeleteComment(productId, commentId);
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product.Id);
        }

        public IEnumerable<Product> Get()
        {
            return _productRepository.Get();
        }

        public Product GetSingleBySku(string sku)
        {
            return _productRepository.GetSingleBySku(sku);
        }

        public Product Update(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
