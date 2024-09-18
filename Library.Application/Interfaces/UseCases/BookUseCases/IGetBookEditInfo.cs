using Library.Application.DTOs.BookDtos.Response;

namespace Library.Application.Interfaces.UseCases.BookUseCases;

public interface IGetBookEditInfo
{
    Task<BookEditInfo> ExecuteAsync(CancellationToken cancellationToken);
}
