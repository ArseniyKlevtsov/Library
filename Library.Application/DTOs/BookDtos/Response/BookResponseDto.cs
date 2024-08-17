﻿namespace Library.Application.DTOs.BookDtos.Response;

public class BookResponseDto
{
    public string? Isbn { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public Guid BookImageId { get; set; }

    public Guid InventoryId { get; set; }

    public Guid AuthorId { get; set; }

    public ICollection<Guid>? GenreIds { get; set; }
    public ICollection<Guid>? RentedBookIds { get; set; }
}
