using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;

namespace Library.Application.UseCases.AuthorsUseCases;

public class DeleteAuthor : IDeleteAuthor
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthor(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _authorRepository = unitOfWork.Authors;
    }

    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(id, cancellationToken);
        if (author == null)
        {
            throw new NotFoundException($"author with id {id} not found");
        }
        await _unitOfWork.Authors.DeleteAsync(author, cancellationToken);
        await _unitOfWork.SaveAsync();
    }
}
