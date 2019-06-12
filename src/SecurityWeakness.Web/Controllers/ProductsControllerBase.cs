using Microsoft.AspNetCore.Mvc;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Services;
using SecurityWeakness.Web.Models.Products;

namespace SecurityWeakness.Web.Controllers
{
    public abstract class ProductsControllerBase : Controller
    {
        protected readonly IProductService ProductService;

        protected ProductsControllerBase(IProductService productService)
        {
            ProductService = productService;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel { Products = ProductService.Get() });
        }

        [HttpGet]
        public IActionResult Product(string sku)
        {
            return View(ProductService.GetSingleBySku(sku));
        }

        [HttpPost]
        public IActionResult AddComment(string sku, [Bind("ProductId,UserEmail,Text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                ProductService.AddComment(comment.ProductId, new Comment
                {
                    Text = comment.Text,
                    UserEmail = comment.UserEmail
                });
            }

            return RedirectToAction(nameof(Product), new { sku });
        }
    }
}
