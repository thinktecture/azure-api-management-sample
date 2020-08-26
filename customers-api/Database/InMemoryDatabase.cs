using System;
using System.Collections.Generic;
using System.Linq;
using CustomersService.Entities;

namespace CustomersService.Database
{
    public class InMemoryDatabase
    {
        private readonly List<Customer> _InMemoryDatabase = new List<Customer>();

        public InMemoryDatabase()
        {
            FillInMemoryDatabase();
        }

        private void FillInMemoryDatabase()
        {
            _InMemoryDatabase.Add(new Customer
            {
                Id = Guid.Parse("b4940e93-fa3b-46bf-8696-27aef1d2ed9e"),
                FirstName = "Thorsten",
                LastName = "Hans",
                City = "Saarbrücken",
                Zip = "66123",
                Country = "Germany",
                CreatedAt = DateTime.UtcNow
            });
        }

        public IEnumerable<Customer> GetAll()
        {
            return _InMemoryDatabase.Where(c=> !c.IsDeleted);
        }

        public void Add(Customer customer)
        {
            _InMemoryDatabase.Add(customer);
        }

        public Customer FirstOrDefault(Guid id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }

        internal int GetCount()
        {
            return GetAll().Count();
        }
    }
}
