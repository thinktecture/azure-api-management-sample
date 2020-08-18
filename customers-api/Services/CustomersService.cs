using System;
using System.Collections.Generic;
using AutoMapper;
using CustomersService.Database;
using CustomersService.Entities;
using CustomersService.Models;

namespace CustomersService.Services
{
    public class CustomersService
    {
        public CustomersService(InMemoryDatabase database, IMapper mapper)
        {
            Database = database;
            Mapper = mapper;
        }

        public InMemoryDatabase Database { get; }
        public IMapper Mapper { get; }

        public IEnumerable<CustomerListModel> GetAllCustomers()
        {
            return Mapper.Map<IEnumerable<CustomerListModel>>(Database.GetAll());
        }


        public CustomerDetailsModel GetCustomerById(Guid id)
        {
            var found = Database.FirstOrDefault(id);
            if(found == null)
            {
                return null;
            }
            return Mapper.Map<CustomerDetailsModel>(found);
        }

        public CustomerDetailsModel UpdateCustomerById(Guid id, CustomerUpdateModel customerUpdateModel)
        {
            var found = Database.FirstOrDefault(id);
            if(found == null)
            {
                return null;
            }
            Mapper.Map<CustomerUpdateModel, Customer>(customerUpdateModel, found);
            return Mapper.Map<CustomerDetailsModel>(found);
        }

        public bool DeleteCustomerById(Guid id)
        {
            var found = Database.FirstOrDefault(id);
            if(found == null)
            {
                return false;
            }
            return found.IsDeleted = true;
        }

        public CustomerDetailsModel CreateCustomer(CustomerCreateModel customerCreateModel)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };
            Mapper.Map<CustomerCreateModel, Customer>(customerCreateModel, customer);
            Database.Add(customer);
            return Mapper.Map<CustomerDetailsModel>(customer);
        }

        internal int GetCustomerCount()
        {
            return Database.GetCount();
        }
    }
}
