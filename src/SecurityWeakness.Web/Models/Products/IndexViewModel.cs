using System.Collections.Generic;
using SecurityWeakness.Domain.Entities;

namespace SecurityWeakness.Web.Models.Products
{
    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

    }
}