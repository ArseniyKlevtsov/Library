using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.AuthorsUseCases;

public class UpdateAuthor: IUpdateAuthor
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthor(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorResponseDto> ExecuteAsync(Guid authorId, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(authorId, cancellationToken);
        if (author == null)
        {
            throw new KeyNotFoundException($"Author with ID {authorId} not found.");
        }

        _mapper.Map(authorRequestDto, author);
        await _unitOfWork.Authors.UpdateAsync(author, cancellationToken);
        await _unitOfWork.SaveAsync();
        var updatedAuthor = _mapper.Map<AuthorResponseDto>(author);
        return updatedAuthor;
    }
}
