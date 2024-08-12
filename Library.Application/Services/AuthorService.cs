using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;

namespace Library.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    public AuthorService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _authorRepository = unitOfWork.Authors;
        _mapper = mapper;
    }

    public async Task<AuthorResponseDto> GetAuthorByIdAsync(Guid authorId, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(authorId, cancellationToken);
        if (author == null) {
            throw new KeyNotFoundException($"Author with ID {authorId} not found.");
        }
        var authorResponseDto = _mapper.Map<AuthorResponseDto>(author);
        return authorResponseDto;
    }

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAuthorsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAsync(cancellationToken);

        var totalCount = authors.Count();
        var skipCount = (pageNumber - 1) * pageSize;
        var pagedAuthors = authors.Skip(skipCount).Take(pageSize);

        var authorResponseDtos = _mapper.Map<IEnumerable<AuthorResponseDto>>(pagedAuthors);
        return authorResponseDtos;
    }

    public async Task CreateAuthorAsync(AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<Author>(authorRequestDto);
        await _authorRepository.AddAsync(author, cancellationToken);
    }

    public async Task UpdateAuthorAsync(Guid authorId, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(authorId, cancellationToken);
        if (author == null)
        {
            throw new KeyNotFoundException($"Author with ID {authorId} not found.");
        }

        _mapper.Map(authorRequestDto, author);
        await _authorRepository.UpdateAsync(author, cancellationToken);
    }
    
    public async Task DeleteAuthorAsync(Guid authorId, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(authorId, cancellationToken);
        if (author == null)
        {
            throw new KeyNotFoundException($"Author with ID {authorId} not found.");
        }

        await _authorRepository.DeleteAsync(author, cancellationToken);
    }

}
