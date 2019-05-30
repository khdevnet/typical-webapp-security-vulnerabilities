using Microsoft.AspNetCore.Mvc;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Web.Models.Products;

namespace SecurityWeakness.Web.Controllers
{
    public class NotSecureProductsController : Controller
    {
        private readonly IProductSqlRepository _productSqlRepository;

        public NotSecureProductsController(IProductSqlRepository productSqlRepository)
        {
            _productSqlRepository = productSqlRepository;
        }

        [HttpGet]
        public IActionResult Search(string name)
        {
            return View(new IndexViewModel { Products = _productSqlRepository.GetByName(name) });
        }
    }
}
