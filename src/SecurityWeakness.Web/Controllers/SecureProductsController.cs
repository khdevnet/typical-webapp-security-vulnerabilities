using SecurityWeakness.Domain.Extensibility.Repositories;

namespace SecurityWeakness.Web.Controllers
{
    public class SecureProductsController : ProductsControllerBase
    {
        public SecureProductsController(ISecureProductSqlRepository productSqlRepository)
            : base(productSqlRepository)
        {
        }
    }
}
