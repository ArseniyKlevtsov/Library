using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.UseCases;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;

namespace Library.Application.UseCases.AuthorsUseCases;

public class UpdateAuthor: IUpdateAuthor
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public UpdateAuthor(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authorRepository = unitOfWork.Authors;
        _mapper = mapper;
    }

    public async Task<AuthorResponseDto> ExecuteAsync(Guid authorId, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(authorId, cancellationToken);
        if (author == null)
        {
            throw new KeyNotFoundException($"Author with ID {authorId} not found.");
        }

        _mapper.Map(authorRequestDto, author);
        await _authorRepository.UpdateAsync(author, cancellationToken);
        await _unitOfWork.SaveAsync();
        var updatedAuthor = _mapper.Map<AuthorResponseDto>(author);
        return _mapper.Map<AuthorResponseDto>(updatedAuthor);
    }
}
