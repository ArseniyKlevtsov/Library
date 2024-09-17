using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;

namespace Library.Application.Interfaces.UseCases.BookUseCases;

public interface IGetBooksPage
{
    Task<BooksResponseDto> ExecuteAsync(GetAllBooksRequestDto getAllBooksRequestDto, CancellationToken cancellationToken);
}
