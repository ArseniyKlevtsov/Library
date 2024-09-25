using AutoMapper;
using Library.Application.DTOs.RentedBookDtos.Request;
using Library.Application.DTOs.RentedBookDtos.Response;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class RentedBookProfile : Profile
{
    public RentedBookProfile()
    {
        CreateMap<RentedBookRequestDto, RentedBook>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.BooksCount, opt => opt.MapFrom(src => src.BooksCount))
            .ForMember(dest => dest.TakeTime, opt => opt.MapFrom(src => src.TakeTime))
            .ReverseMap();

        CreateMap<RentedBook, RentedbookResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.BooksCount, opt => opt.MapFrom(src => src.BooksCount))
            .ForMember(dest => dest.TakeTime, opt => opt.MapFrom(src => src.TakeTime))
            .ForMember(dest => dest.ReturnTime, opt => opt.MapFrom(src => src.ReturnTime))
            .ReverseMap();

        CreateMap<RentedBook, RentedBookProfileResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.BooksCount, opt => opt.MapFrom(src => src.BooksCount))
            .ForMember(dest => dest.TakeTime, opt => opt.MapFrom(src => src.TakeTime))
            .ForMember(dest => dest.ReturnTime, opt => opt.MapFrom(src => src.ReturnTime))
            .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book!.Name))
            .ReverseMap();
    }
}
