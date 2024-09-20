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
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ReverseMap();

        CreateMap<Book, BookResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.BookImage!.Image!)))
            .ForMember(dest => dest.AvailableCount, opt => opt.MapFrom(src => src.Inventory!.AvailableCount))
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.Inventory!.TotalCount))
            .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.GenreIds, opt => opt.MapFrom(src => src.Genres!.Select(genre => genre.Id)))
            .ForMember(dest => dest.RentedBookIds, opt => opt.MapFrom(src => src.RentedBooks!.Select(rentedBook => rentedBook.Id)));

        CreateMap<Book, BookPreviewResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AvailableCount, opt => opt.MapFrom(src => src.Inventory!.AvailableCount))
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.Inventory!.TotalCount))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.BookImage!.Image!)));

        CreateMap<Book, BookInfoResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.AvailableCount, opt => opt.MapFrom(src => src.Inventory!.AvailableCount))
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.Inventory!.TotalCount))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.BookImage!.Image!)));
    }

}
