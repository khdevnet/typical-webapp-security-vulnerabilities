using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Domain.Extensibility.Services;

namespace SecurityWeakness.Domain.Services
{
    internal class NotSecureProductService : ProductServiceBase, INotSecureProductService
    {
        private readonly INotSecureProductRepository _productRepository;

        public NotSecureProductService(INotSecureProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
