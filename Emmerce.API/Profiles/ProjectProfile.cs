using AutoMapper;
using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;

namespace Ecommerce.API.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ApplicationUser, DisplayFindUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, SignUp>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDto>().ReverseMap();
            CreateMap<Product, DisplayProductDto>().ReverseMap();
            CreateMap<Product, AddProductDto>().ReverseMap();
            CreateMap<ProductSpecification, AddProductSpecificationDto>().ReverseMap();
            CreateMap<ProductSpecification, DisplayProductSpecificationDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, DisplayProduct>().ReverseMap();
            CreateMap<Cart, DisplayCart>();
            CreateMap<CartItem, DisplayCartItemDto>().ReverseMap();
            CreateMap<DisplayDeliveryAddressDto, Delivery>().ReverseMap();
            CreateMap<Delivery, AddDeliveryDto>()
                 .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.ReceiverState))
                 .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.ReceiverCity))
                 .ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.ReceiverPhone))
                 .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.ReceiverStreet))
                 .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ReceiverPostalCode))
                 .ReverseMap();
            CreateMap<Delivery, UpdateDeliveryDto>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.ReceiverState))
                 .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.ReceiverCity))
                 .ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.ReceiverPhone))
                 .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.ReceiverStreet))
                 .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ReceiverPostalCode))
                 .ReverseMap();
            CreateMap<Delivery, ShippingRequestPayload>().
                ForMember(dest => dest.destination_state, opt => opt.MapFrom(src => src.ReceiverState))
                 .ForMember(dest => dest.destination_city, opt => opt.MapFrom(src => src.ReceiverCity))
                 .ForMember(dest => dest.destination_phone, opt => opt.MapFrom(src => src.ReceiverPhone))
                 .ForMember(dest => dest.destination_street, opt => opt.MapFrom(src => src.ReceiverStreet))
                 .ForMember(dest => dest.destination_name, opt => opt.MapFrom(src => src.ReceiverName))
                 .ForMember(dest => dest.destination_country, opt => opt.MapFrom(src => src.ReceiverCountry))
                 .ForMember(dest => dest.origin_state, opt => opt.MapFrom(src => src.SenderState))
                 .ForMember(dest => dest.origin_city, opt => opt.MapFrom(src => src.SenderCity))
                 .ForMember(dest => dest.origin_phone, opt => opt.MapFrom(src => src.SenderPhone))
                 .ForMember(dest => dest.origin_street, opt => opt.MapFrom(src => src.SenderStreet))
                 .ForMember(dest => dest.origin_name, opt => opt.MapFrom(src => src.SenderName))
                 .ForMember(dest => dest.origin_country, opt => opt.MapFrom(src => src.SenderCountry))
                 .ReverseMap();
            CreateMap<Order, AddOrderDto>().ReverseMap();
            CreateMap<Order, DisplayOrderDto>().ReverseMap();
        }
    }
}
