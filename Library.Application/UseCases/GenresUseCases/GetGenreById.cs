﻿using AutoMapper;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Library.Infrastructure;

namespace Library.Application.UseCases.GenresUseCases;

public class GetGenreById: IGetGenreById
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGenreById(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GenreResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(id, cancellationToken);
        if (genre == null)
        {
            throw new NotFoundException($"Genre with ID {id} not found");
        }
        return _mapper.Map<GenreResponseDto>(genre);
    }
}
