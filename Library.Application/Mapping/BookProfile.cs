using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookRequestDto, Book>()
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BookImageId, opt => opt.MapFrom(src => src.BookImageId))
            .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ReverseMap();

        CreateMap<Book, BookResponseDto>()
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BookImageId, opt => opt.MapFrom(src => src.BookImageId))
            .ForMember(dest => dest.BookImage, opt => opt.MapFrom(src => src.BookImage))
            .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
            .ForMember(dest => dest.Inventory, opt => opt.MapFrom(src => src.Inventory))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.RentedBooks, opt => opt.MapFrom(src => src.RentedBooks))
            .ReverseMap();
    }

}
