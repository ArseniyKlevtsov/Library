using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class LibraryInventoryProfile : Profile
{
    public LibraryInventoryProfile()
    {
        CreateMap<BookRequestDto, LibraryInventory>()
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount ?? 0))
            .ForMember(dest => dest.AvailableCount, opt => opt.MapFrom(src => src.AvailableCount ?? 0))
            .ReverseMap();
    }
}
