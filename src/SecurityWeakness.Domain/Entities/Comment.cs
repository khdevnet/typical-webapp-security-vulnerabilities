using System;

namespace SecurityWeakness.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string UserEmail { get; set; }

        public string Text { get; set; }
    }
}