using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.GenreDtos.Request;
using Library.Domain.SearchCriterias;
using Library.Domain.SearchCriteries;

namespace Library.Application.Mapping;

public class CriteriesProfile : Profile
{
    public CriteriesProfile() 
    {
        CreateMap<GetAllAuthorsRequestDto, AuthorCriterias>().ReverseMap();

        CreateMap<GetAllBooksRequestDto, BookCriterieas>().ReverseMap();

        CreateMap<GetAllGenresRequestDto, GenreCriterias>().ReverseMap();
    }
}
