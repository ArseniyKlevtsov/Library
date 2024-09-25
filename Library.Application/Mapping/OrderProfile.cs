using AutoMapper;
using Library.Application.DTOs.OrderDtos.Respose;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<RentOrder, OrderResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.RentedBooks, opt => opt.MapFrom(src => src.RentedBooks))
            .ReverseMap();
    }
}
