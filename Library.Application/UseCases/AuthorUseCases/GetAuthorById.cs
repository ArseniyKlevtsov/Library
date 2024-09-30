using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.AuthorsUseCases;

public class GetAuthorById : IGetAuthorById
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorById(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<AuthorResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var includeState = new AuthorIncludeState()
        {
            IncludeBooks = true
        };

        var authors = await _unitOfWork.Authors.GetWithIncludeByPredicateAsync(author => author.Id == id, includeState, cancellationToken);
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
