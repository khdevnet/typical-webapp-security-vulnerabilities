using Microsoft.AspNetCore.Mvc;
using SecurityWeakness.Domain.Entities;
using SecurityWeakness.Domain.Extensibility.Repositories;
using SecurityWeakness.Web.Models.Products;

namespace SecurityWeakness.Web.Controllers
{
    public abstract class ProductsControllerBase : Controller
    {
        protected readonly IProductSqlRepository ProductSqlRepository;
        protected readonly ICrudRepository<Comment, int> CommentRepository;

        protected ProductsControllerBase(IProductSqlRepository productSqlRepository, ICrudRepository<Comment, int> commentRepository)
        {
            ProductSqlRepository = productSqlRepository;
            CommentRepository = commentRepository;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel { Products = ProductSqlRepository.Get() });
        }

        [HttpGet]
        public IActionResult Product(string sku)
        {
            return View(ProductSqlRepository.GetSingleBySku(sku));
        }

        [HttpPost]
        public IActionResult AddComment(string sku, [Bind("ProductId,UserEmail,Text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                CommentRepository.Add(new Comment
                {
                    ProductId = comment.ProductId,
                    Text = comment.Text,
                    UserEmail = comment.UserEmail
                });
            }

            return RedirectToAction(nameof(Product), new { sku });
        }
    }
}
