using AutoMapper;
using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.UserDtos.Request;
using Library.Application.DTOs.UserDtos.Response;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, LoginRequestDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<User, RegisterRequestDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ReverseMap();

        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.RentOrders, opt => opt.MapFrom(src => src.RentOrders))
            .ReverseMap();

        CreateMap<UserRequestDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.NewUserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.NewEmail))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.NewPhoneNumber))
            .ReverseMap();
    }
}
