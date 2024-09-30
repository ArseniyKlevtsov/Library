using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.UseCases.AuthorsUseCases;

public class CreateAuthor : ICreateAuthor
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthor(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authorRepository = unitOfWork.Authors;
        _mapper = mapper;
    }

    public async Task<AuthorResponseDto> ExecuteAsync (AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<Author>(authorRequestDto);
        await _authorRepository.AddAsync(author, cancellationToken);
        await _unitOfWork.SaveAsync();
        var createdAuthor = _mapper.Map<AuthorResponseDto>(author);
        return createdAuthor;
    }
}
