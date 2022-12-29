using AutoMapper;
using CleanArchitectureDemo.Application.Features.Customers.Commands.AddUpdate;
using CleanArchitectureDemo.Application.Features.Customers.Queries.GetAll;
using CleanArchitectureDemo.Application.Features.Customers.Queries.GetById;
using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Application.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<AddUpdateCustomerCommand, Customer>().ReverseMap();
        CreateMap<GetCustomerByIdResponse, Customer>().ReverseMap();
        CreateMap<GetAllCustomersResponse, Customer>().ReverseMap();
    }
}