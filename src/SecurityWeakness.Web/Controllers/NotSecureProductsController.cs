using Microsoft.AspNetCore.Mvc;
using SecurityWeakness.Domain.Extensibility.Services;

namespace SecurityWeakness.Web.Controllers
{
    public class NotSecureProductsController : ProductsControllerBase
    {
        public NotSecureProductsController(INotSecureProductService productService)
            : base(productService)
        {
        }
    }
}
