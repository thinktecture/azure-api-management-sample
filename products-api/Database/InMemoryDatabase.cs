using System;
using System.Collections.Generic;
using System.Linq;
using ProductsService.Entities;

namespace ProductsService.Database
{
    public class InMemoryDatabase
    {
        private readonly List<Product> _InMemoryDatabase = new List<Product>();

        public InMemoryDatabase()
        {
            FillInMemoryDatabase();
        }

        private void FillInMemoryDatabase()
        {
            _InMemoryDatabase.Add(new Product
            {
                Id = Guid.Parse("aa940e93-fa3b-46bf-8696-27aef1d2ed11"),
                Name = "Meat",
                Description = "Ahh fresh meat",
                Price = 7.99,
                CreatedAt = DateTime.UtcNow
            });
        }

        public IEnumerable<Product> GetAll()
        {
            return _InMemoryDatabase.Where(p => !p.IsDeleted);
        }

        public void Add(Product product)
        {
            _InMemoryDatabase.Add(product);
        }

        public Product FirstOrDefault(Guid id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        internal int GetCount()
        {
            return GetAll().Count();
        }
    }
}
