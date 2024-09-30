using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Library.Tests.RepositoriesTests;

public class AuthorRepositoryTests
{
    [Fact]
    public async Task CrudOperations_Test()
    {
        using (var context = new LibraryDbContextTests())
        {
            // Arrange
            context.ChangeTracker.Clear();
            var authorRepository = new AuthorRepository(context);

            // Act - Create
            var newAuthor = new Author { Name = "John", Surname = "Doe", BirthDate = new DateOnly(1980, 1, 1), Country = "USA" };
            await authorRepository.AddAsync(newAuthor, CancellationToken.None);
            // вместо UnitOfWork.SaveAsync(), потому что не понял как создать для User/Role Manager User/Role Store
            context.SaveChanges();
            context.Entry(newAuthor).State = EntityState.Detached;

            // Assert - Create
            var createdAuthor = await authorRepository.GetByPredicateAsync(a => a.Name == "John" && a.Surname == "Doe", CancellationToken.None);
            Assert.NotNull(createdAuthor);
            Assert.Equal("John", createdAuthor.Name);
            Assert.Equal("Doe", createdAuthor.Surname);
            Assert.Equal(new DateOnly(1980, 1, 1), createdAuthor.BirthDate);
            Assert.Equal("USA", createdAuthor.Country);

            // Act - Read
            var allAuthors = await authorRepository.GetAllAsync(CancellationToken.None);
            Assert.Single(allAuthors);
            Assert.Contains(allAuthors, a => a.Id == createdAuthor.Id);

            // Act - Update
            createdAuthor.Name = "Jane";
            await authorRepository.UpdateAsync(createdAuthor, CancellationToken.None);
            context.SaveChanges();

            // Assert - Update
            var updatedAuthor = await authorRepository.GetByPredicateAsync(a => a.Id == createdAuthor.Id, CancellationToken.None);
            Assert.NotNull(updatedAuthor);
            Assert.Equal("Jane", updatedAuthor.Name);
            Assert.Equal("Doe", updatedAuthor.Surname);
            Assert.Equal(new DateOnly(1980, 1, 1), updatedAuthor.BirthDate);
            Assert.Equal("USA", updatedAuthor.Country);

            // Act - Delete
            await authorRepository.DeleteAsync(createdAuthor, CancellationToken.None);

            // Assert - Delete
            var deletedAuthor = await authorRepository.GetByPredicateAsync(a => a.Id == createdAuthor.Id, CancellationToken.None);
            Assert.Null(deletedAuthor);
        }
    }

}