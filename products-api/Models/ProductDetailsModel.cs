using System;
namespace ProductsService.Models
{
    public class ProductDetailsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
