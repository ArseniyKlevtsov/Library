namespace Library.Application.DTOs.RentedBookDtos.Response;

public class RentedBookProfileResponseDto
{
    public Guid Id { get; set; }
    public DateTime? TakeTime { get; set; }
    public DateTime? ReturnTime { get; set; }
    public int BooksCount { get; set; }

    public Guid BookId { get; set; }
    public Guid RentOrderId { get; set; }

    public string? BookName { get; set; }
}
