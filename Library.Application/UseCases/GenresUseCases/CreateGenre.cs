using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Library.Domain.Entities;
using Library.Infrastructure;
using System.Threading;

namespace Library.Application.UseCases.GenresUseCases;

public class CreateGenre : ICreateGenre
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGenre(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<GenreResponseDto> ExecuteAsync(GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        var genre = _mapper.Map<Genre>(genreRequestDto);
        await _unitOfWork.Genres.AddAsync(genre, cancellationToken);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<GenreResponseDto>(genre);
    }
}
