using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Entities;
using Library.Domain.IncludeStates;
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
        var includeState = new AuthorIncludeState()
        {
            IncludeBooks = true
        };

        var authors = await _authorRepository.GetWithIncludeByPredicateAsync(author => author.Id == id, includeState, cancellationToken);
        var author = authors.FirstOrDefault();
        if (author == null)
        {
            throw new NotFoundException($"Author with ID {id} not found.");
        }
        var authorResponseDto1 = _mapper.Map<AuthorResponseDto>(author);
        Console.WriteLine(authorResponseDto1.Country);
        return authorResponseDto1;
    }
}
