using Aplication.DTOs.Category;
using Aplication.DTOs.Order;
using Aplication.DTOs.Permission;
using Aplication.DTOs.Product;
using Aplication.DTOs.Roles;
using Aplication.DTOs.User;
using Aplication.DTOs.Waiter;
using AutoMapper;
using Domain.Models.Models;
using Domain.Models.ModelsJwt;

namespace Aplication.Mappings
{
    public class MapProfiles : Profile
    {
        public MapProfiles() 
        {
            CreateMap<CategoryCreateDTO, Categories>().ReverseMap();
            CreateMap<CategoryGetDTO, Categories>().ReverseMap();
            CreateMap<CategoryUpdateDTO, Categories>().ReverseMap();

            CreateMap<OrderCreateDTO, Orders>()
              .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Select(id => new Products() { ProductId = id }).ToList()));
               
            CreateMap<OrderGetDTO, Orders>().ReverseMap();
            CreateMap<OrderUpdateDTO, Orders>().ReverseMap();

            CreateMap<PermissionCreateDTO, Permission>().ReverseMap();
            CreateMap<PermissionUpdateDTO, Permission>().ReverseMap();
            CreateMap<PermissionGetDTO, Permission>().ReverseMap();

            CreateMap<ProductCreateDTO, Products>().ReverseMap();
            CreateMap<ProductUpdateDTO, Products>().ReverseMap();
            CreateMap<ProductGetDTO, Products>().ReverseMap();

            CreateMap<RoleCreateDTO, Roles>().ReverseMap();
            CreateMap<RoleUpdateDTO, Roles>().ReverseMap();
            CreateMap<RoleGetDTO, Roles>().ReverseMap();

            CreateMap<UserCreateDTO, Users>().ReverseMap();
            CreateMap<UserUpdateDTO, Users>().ReverseMap();
            CreateMap<UserGetDTO, Users>().ReverseMap();

            CreateMap<WaiterCreateDTO, Waiter>()
             .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders.Select(id => new Orders() { OrderId = id }).ToList()));

            CreateMap<WaiterUpdateDTO, Waiter>().ReverseMap();
            CreateMap<WaiterGetDTO, Waiter>().ReverseMap();

            CreateMap<WaiterCreateDTO, Waiter>().ReverseMap();
            CreateMap<WaiterUpdateDTO, Waiter>().ReverseMap();
            CreateMap<WaiterGetDTO, Waiter>().ReverseMap();

        }
    }
}
