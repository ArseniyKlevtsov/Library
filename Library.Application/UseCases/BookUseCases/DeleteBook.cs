using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.BooksUseCases;

public class DeleteBook: IDeleteBook
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBook(IUnitOfWork unitOfWork)
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
        var bookInventory = await _unitOfWork.LibraryInventorys.GetByIdAsync(book.InventoryId, cancellationToken);
        var bookImage = await _unitOfWork.BookImages.GetByIdAsync(book.BookImageId, cancellationToken);
        await _unitOfWork.Books.DeleteAsync(book, cancellationToken);
        await _unitOfWork.BookImages.DeleteAsync(bookImage!, cancellationToken);
        await _unitOfWork.LibraryInventorys.DeleteAsync(bookInventory!, cancellationToken);
        await _unitOfWork.SaveAsync();
    }
}
