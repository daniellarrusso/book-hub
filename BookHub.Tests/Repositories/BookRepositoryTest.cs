using BookHub.Core.Repositories;
using BookHub.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHub.Tests;

public class BookRepositoryTest
{
  private BookContext GetInMemoryDbContext()
  {
    var options = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

    var context = new BookContext(options);

    context.Books.AddRange(
        new Book { Id = 1, Title = "1984", Author = "George Orwell" },
        new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin" },
        new Book { Id = 3, Title = "Brave New World", Author = "Aldous Huxley" }
    );

    context.SaveChanges();
    return context;

  }

  [Fact]
  public async Task GetBooks_ReturnsPagedResults()
  {
    using var context = GetInMemoryDbContext();
    var repo = new BookRepository(context);

    var result = await repo.GetBooks(page: 1, pageSize: 2, search: null, sort: null);

    Assert.Equal(2, result.Items.Count());
    Assert.Equal(4, result.TotalCount);
    Assert.Equal(2, result.TotalPages);
    Assert.Equal(1, result.Page);
    Assert.Equal(2, result.PageSize);
  }

  [Fact]
  public async Task GetBooks_AppliesSearchFilter()
  {
    using var context = GetInMemoryDbContext();
    var repo = new BookRepository(context);

    var result = await repo.GetBooks(page: 1, pageSize: 10, search: "code", sort: null);

    Assert.Equal(2, result.Items.Count());
    Assert.All(result.Items, b => Assert.Contains("code", b.Title.ToLower()));
  }

}
