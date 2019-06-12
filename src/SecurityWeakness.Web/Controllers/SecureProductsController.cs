using SecurityWeakness.Domain.Extensibility.Services;

namespace SecurityWeakness.Web.Controllers
{
    public class SecureProductsController : ProductsControllerBase
    {
        public SecureProductsController(ISecureProductService productService)
            : base(productService)
        {
        }
    }
}
