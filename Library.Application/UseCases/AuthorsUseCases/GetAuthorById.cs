using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;

namespace Library.Application.UseCases.AuthorsUseCases;

public class GetAuthorById : IGetAuthorById
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public GetAuthorById(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authorRepository = unitOfWork.Authors;
        _mapper = mapper;
    }
    public async Task<AuthorResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(id, cancellationToken);
        if (author == null)
        {
            throw new NotFoundException($"Author with ID {id} not found.");
        }
        var authorResponseDto = _mapper.Map<AuthorResponseDto>(author);
        return authorResponseDto;
    }
}
