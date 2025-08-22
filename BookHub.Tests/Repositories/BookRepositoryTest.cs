using BookHub.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookHub.Tests;

public class BookRepositoryTest
{
  private async Task<BookContext> GetInMemoryDbContext()
  {
    var options = new DbContextOptionsBuilder<BookContext>()
      .UseInMemoryDatabase(databaseName: "BookDb_" + System.Guid.NewGuid())
      .Options;

    var context = new BookContext(options);
    context.Books.AddRange(
      new Book { Id = 1, Title = "Clean Code" },
      new Book { Id = 2, Title = "System Design" }
    );

    await context.SaveChangesAsync();
    return context;

  }

  [Fact]
  public async Task ShouldReturnListOfBooksFromDatabase()
  {
    var context = await GetInMemoryDbContext();
    IBookRepository repo = new BookRepository(context);

    var books = await repo.GetBooks();

    Assert.Equal(2, books.Count());
  }

}
