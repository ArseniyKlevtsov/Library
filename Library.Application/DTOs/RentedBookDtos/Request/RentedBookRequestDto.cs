namespace Library.Application.DTOs.RentedBookDtos.Request;

public class RentedBookRequestDto
{
    public Guid BookId { get; set; }
    public int BooksCount { get; set; }
    public DateTime TakeTime { get; set; }

}
