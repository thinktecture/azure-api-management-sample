using System;
using AutoMapper;
using CustomersService.Entities;
using CustomersService.Models;

namespace CustomersService.Configuration
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerListModel>()
                .ForMember(c => c.Name, opt => opt.MapFrom(src => $"{src.LastName}, {src.FirstName}"));
            CreateMap<Customer, CustomerDetailsModel>();
            CreateMap<CustomerCreateModel, Customer>();
            CreateMap<CustomerUpdateModel, Customer>();
        }
    }
}
