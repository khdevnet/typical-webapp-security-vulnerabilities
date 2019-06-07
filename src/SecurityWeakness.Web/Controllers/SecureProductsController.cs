using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;

namespace SecurityWeakness.Web.Controllers
{
    public class SecureProductsController : ProductsControllerBase
    {
        public SecureProductsController(ISecureProductSqlRepository productSqlRepository, ICrudRepository<Comment, int> commentRepository)
            : base(productSqlRepository, commentRepository)
        {
        }
    }
}
