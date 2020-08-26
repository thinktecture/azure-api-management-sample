using System;
namespace ProductsService.Models
{
    public class ProductListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
