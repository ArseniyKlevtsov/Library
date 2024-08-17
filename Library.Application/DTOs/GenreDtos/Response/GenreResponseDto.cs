﻿namespace Library.Application.DTOs.GenreDtos.Response;

public class GenreResponseDto
{
    public string? Name { get; set; }

    public ICollection<Guid>? BookIds { get; set; }
}
