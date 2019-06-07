using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;

namespace SecurityWeakness.Web.Controllers
{
    public class NotSecureProductsController : ProductsControllerBase
    {
        public NotSecureProductsController(INotSecureProductSqlRepository productSqlRepository, ICrudRepository<Comment, int> commentRepository)
            : base(productSqlRepository, commentRepository)
        {
        }
    }
}
