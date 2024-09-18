using Library.Domain.Entities;

namespace Library.Application.DTOs.BookDtos.Response;

public class BookEditInfo
{
    public IEnumerable<Genre>? Genres { get; set; }
    public IEnumerable<Author>? Authors { get; set; }
}
