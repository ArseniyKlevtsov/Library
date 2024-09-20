using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class BookImageProfile: Profile
{
    public BookImageProfile() 
    {
        CreateMap<BookRequestDto, BookImage>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Convert.FromBase64String(src.Image) : null));
    }
}
