using System;
using System.Collections.Generic;
using AutoMapper;
using ProductsService.Database;
using ProductsService.Entities;
using ProductsService.Models;

namespace ProductsService.Services
{
    public class ProductsService
    {
        public ProductsService(InMemoryDatabase database, IMapper mapper)
        {
            Database = database;
            Mapper = mapper;
        }

        public InMemoryDatabase Database { get; }
        public IMapper Mapper { get; }

        public IEnumerable<ProductListModel> GetAllProducts()
        {
            return Mapper.Map<IEnumerable<ProductListModel>>(Database.GetAll());
        }


        public ProductDetailsModel GetProductById(Guid id)
        {
            var found = Database.FirstOrDefault(id);
            if (found == null)
            {
                return null;
            }
            return Mapper.Map<ProductDetailsModel>(found);
        }

        public ProductDetailsModel UpdateProductById(Guid id, ProductUpdateModel productUpdateModel)
        {
            var found = Database.FirstOrDefault(id);
            if (found == null)
            {
                return null;
            }
            Mapper.Map(productUpdateModel, found);
            return Mapper.Map<ProductDetailsModel>(found);
        }

        public bool DeleteProductById(Guid id)
        {
            var found = Database.FirstOrDefault(id);
            if (found == null)
            {
                return false;
            }
            return found.IsDeleted = true;
        }

        public ProductDetailsModel CreateProduct(ProductCreateModel productCreateModel)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };
            Mapper.Map(productCreateModel, product);
            Database.Add(product);
            return Mapper.Map<ProductDetailsModel>(product);
        }

        internal int GetProductCount()
        {
            return Database.GetCount();
        }
    }
}
