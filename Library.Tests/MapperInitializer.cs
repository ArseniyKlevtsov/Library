using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Domain.Entities;

namespace Library.Application;

public class MapperInitializer
{
    private IMapper _mapper;

    public MapperInitializer()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AuthorRequestDto, Author>();
            cfg.CreateMap<Author, AuthorResponseDto>();
        });

        _mapper = config.CreateMapper();
    }

    public IMapper Mapper => _mapper;
}