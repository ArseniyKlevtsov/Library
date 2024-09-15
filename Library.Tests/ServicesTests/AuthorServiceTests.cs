using Library.Application;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.Services;
using Library.Infrastructure;
using Library.Infrastructure.Repositories;
using Xunit;

namespace Library.Tests.ServicesTests;

public class AuthorServiceTests
{
    /*

    [Fact]
    public async Task AuthorService_CrudOperations_Test()
    {
        // Arrange
        using (var context = new LibraryDbContextTests())
        {
            context.ChangeTracker.Clear();
            var authorRepository = new AuthorRepository(context);
            var authorService = new AuthorService(new UnitOfWork(context), new MapperInitializer().Mapper);

            // Act - Create
            var authorRequestDto = new AuthorRequestDto
            {
                Name = "John",
                Surname = "Doe",
                BirthDate = new DateOnly(1980, 1, 1),
                Country = "USA"
            };
            await authorService.CreateAuthorAsync(authorRequestDto, CancellationToken.None);

            // Assert - Create
            var unsureId = context.Authors.FirstOrDefault().Id;
            var createdAuthor = await authorService.GetAuthorByIdAsync(unsureId, CancellationToken.None);
            Assert.NotNull(createdAuthor);
            Assert.Equal("John", createdAuthor.Name);
            Assert.Equal("Doe", createdAuthor.Surname);
            Assert.Equal(new DateOnly(1980, 1, 1), createdAuthor.BirthDate);
            Assert.Equal("USA", createdAuthor.Country);

            // Act - Read
            var allAuthors = await authorService.GetAllAuthorsAsync(1, 10, CancellationToken.None);
            Assert.Single(allAuthors);
            Assert.Contains(allAuthors, a => a.Name == "John" && a.Surname == "Doe" && a.Country == "USA");

            // Act - Update
            var updatedAuthorRequestDto = new AuthorRequestDto
            {
                Name = "Jane",
                Surname = createdAuthor.Surname,
                BirthDate = createdAuthor.BirthDate,
                Country = createdAuthor.Country
            };
            context.ChangeTracker.Clear();
            await authorService.UpdateAuthorAsync(unsureId, updatedAuthorRequestDto, CancellationToken.None);

            // Assert - Update
            var updatedAuthor = await authorService.GetAuthorByIdAsync(unsureId, CancellationToken.None);
            Assert.NotNull(updatedAuthor);
            Assert.Equal("Jane", updatedAuthor.Name);
            Assert.Equal("Doe", updatedAuthor.Surname);
            Assert.Equal(new DateOnly(1980, 1, 1), updatedAuthor.BirthDate);
            Assert.Equal("USA", updatedAuthor.Country);

            // Act - Delete
            context.ChangeTracker.Clear();
            await authorService.DeleteAuthorAsync(unsureId, CancellationToken.None);

            // Assert - Delete
            context.ChangeTracker.Clear();
            var exception = await Assert.ThrowsAnyAsync<Exception>(() => authorService.GetAuthorByIdAsync(unsureId, CancellationToken.None));
            Assert.IsType<KeyNotFoundException>(exception);
        }

    }
        */
}