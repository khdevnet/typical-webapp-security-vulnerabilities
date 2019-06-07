using System.Collections.Generic;

namespace SecurityWeakness.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}