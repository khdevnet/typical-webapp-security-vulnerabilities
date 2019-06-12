using SecurityWeakness.Core.Extensibility;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Domain.Extensibility.Services;

namespace SecurityWeakness.Domain.Services
{
    internal class SecureProductService : ProductServiceBase, ISecureProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public SecureProductService(ISecureProductRepository productRepository, IHtmlSanitizer htmlSanitizer) : base(productRepository)
        {
            _productRepository = productRepository;
            _htmlSanitizer = htmlSanitizer;
        }

        public new void AddComment(int productId, Comment comment)
        {
            comment.Text = _htmlSanitizer.Sanitize(comment.Text);
            _productRepository.AddComment(productId, comment);
        }
    }
}
