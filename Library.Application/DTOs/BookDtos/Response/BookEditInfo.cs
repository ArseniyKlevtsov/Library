using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.DTOs.GenreDtos.Response;

namespace Library.Application.DTOs.BookDtos.Response;

public class BookEditInfo
{
    public IEnumerable<GenreResponseDto>? Genres { get; set; }
    public IEnumerable<AuthorResponseDto>? Authors { get; set; }
}
