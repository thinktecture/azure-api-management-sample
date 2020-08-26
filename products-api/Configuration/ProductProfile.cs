using System;
using AutoMapper;
using ProductsService.Entities;
using ProductsService.Models;

namespace ProductsService.Configuration
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListModel>();
            CreateMap<Product, ProductDetailsModel>();
            CreateMap<ProductCreateModel, Product>();
            CreateMap<ProductUpdateModel, Product>();
        }
    }
}
