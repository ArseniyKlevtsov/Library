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

    public async Task<AuthorsResponseDto> GetAllAuthorsAsync(GetAllAuthorsRequestDto request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAsync(cancellationToken);

        // filtration
        if (!string.IsNullOrEmpty(request.NameFilter))
        {
            var nameFilter = request.NameFilter.ToLower();
            authors = authors.Where(a => a.Name!.ToLower().Contains(nameFilter));
        }
        if (!string.IsNullOrEmpty(request.SurnameFilter))
        {
            var surnameFilter = request.SurnameFilter.ToLower();
            authors = authors.Where(a => a.Surname!.ToLower().Contains(surnameFilter));
        }
        if (request.BirthDateFilter.HasValue)
        {
            var birthDateFilter = request.BirthDateFilter.Value;
            authors = authors.Where(a => a.BirthDate == birthDateFilter);
        }
        if (!string.IsNullOrEmpty(request.CountryFilter))
        {
            var countryFilter = request.CountryFilter.ToLower();
            authors = authors.Where(a => a.Country!.ToLower().Contains(countryFilter));
        }

        // sorting
        if (request.NameSortAsc.HasValue)
        {
            authors = request.NameSortAsc.Value
                ? authors.OrderBy(a => a.Name)
                : authors.OrderByDescending(a => a.Name);
        }
        if (request.SurnameSortAsc.HasValue)
        {
            authors = request.SurnameSortAsc.Value
                ? authors.OrderBy(a => a.Surname)
                : authors.OrderByDescending(a => a.Surname);
        }
        if (request.BirthDateSortAsc.HasValue)
        {
            authors = request.BirthDateSortAsc.Value
                ? authors.OrderBy(a => a.BirthDate)
                : authors.OrderByDescending(a => a.BirthDate);
        }
        if (request.CountrySortAsc.HasValue)
        {
            authors = request.CountrySortAsc.Value
                ? authors.OrderBy(a => a.Country)
                : authors.OrderByDescending(a => a.Country);
        }

        // pagination
        var totalCount = authors.Count();
        var skipCount = (request.Page - 1) * request.PageSize;
        var pagedAuthors = authors.Skip(skipCount).Take(request.PageSize);

        var authorResponseDtos = _mapper.Map<IEnumerable<AuthorResponseDto>>(pagedAuthors);
        return new AuthorsResponseDto
        {
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
            Authors = authorResponseDtos
        };
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
