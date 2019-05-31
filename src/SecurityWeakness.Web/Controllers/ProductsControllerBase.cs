using Microsoft.AspNetCore.Mvc;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Web.Models.Products;

namespace SecurityWeakness.Web.Controllers
{
    public abstract class ProductsControllerBase : Controller
    {
        private readonly IProductSqlRepository _productSqlRepository;

        protected ProductsControllerBase(IProductSqlRepository productSqlRepository)
        {
            _productSqlRepository = productSqlRepository;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel { Products = _productSqlRepository.Get() });
        }

        [HttpGet]
        public IActionResult Product(string sku)
        {
            return View(_productSqlRepository.GetSingleBySku(sku));
        }
    }
}
