using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Infrastructure;

namespace Library.Application.UseCases.BooksUseCases;

public class DeleteBook: IDeleteBook
{
    private readonly UnitOfWork _unitOfWork;

    public DeleteBook(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id, cancellationToken);
        if (book == null)
        {
            throw new NotFoundException($"book with id {id} not found");
        }
        await _unitOfWork.Books.DeleteAsync(book, cancellationToken);
        await _unitOfWork.SaveAsync();
    }
}
