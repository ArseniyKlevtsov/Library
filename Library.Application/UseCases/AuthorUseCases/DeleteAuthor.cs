using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Interfaces;


namespace Library.Application.UseCases.AuthorsUseCases;

public class DeleteAuthor : IDeleteAuthor
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthor(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id, cancellationToken);
        if (author == null)
        {
            throw new NotFoundException($"author with id {id} not found");
        }
        await _unitOfWork.Authors.DeleteAsync(author, cancellationToken);
        await _unitOfWork.SaveAsync();
    }
}
