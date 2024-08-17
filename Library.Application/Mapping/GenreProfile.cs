using AutoMapper;
using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Domain.Entities;

namespace Library.Application.Mapping;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<GenreRequestDto, Genre>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        CreateMap<Genre, GenreResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.Books!.Select(book => book.Id)));
    }
}
