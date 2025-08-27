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
    Assert.Equal(3, result.TotalCount);
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

    Assert.Equal(1, result.Items.Count());
    Assert.All(result.Items, b => Assert.Contains("code", b.Title.ToLower()));
  }

  [Fact]
  public async Task Add_AddsBook_WhenValid()
  {
    using var context = GetInMemoryDbContext();
    var repo = new BookRepository(context);

    var book = new Book { Id = 99, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Rating = 5, Comments = "Great book" };
    var result = await repo.Add(book);

    Assert.NotNull(result);
    Assert.Equal("The Pragmatic Programmer", result.Title);
    Assert.Equal(4, context.Books.Count()); // 3 seeded + 1 added
  }

  [Fact]
  public async Task Add_Throws_WhenMaxBooksReached()
  {
    using var context = GetInMemoryDbContext();
    var repo = new BookRepository(context);

    for (int i = 4; i <= 25; i++)
    {
      context.Books.Add(new Book { Id = i, Title = $"Book {i}", Author = "Test", Rating = 3, Comments = "OK" });
    }
    await context.SaveChangesAsync();

    var newBook = new Book { Id = 26, Title = "Overflow", Author = "Author", Rating = 3, Comments = "Fine" };

    await Assert.ThrowsAsync<InvalidOperationException>(() => repo.Add(newBook));
  }

  [Theory]
  [InlineData(0)]
  [InlineData(6)]
  public async Task Add_Throws_WhenRatingOutOfRange(int rating)
  {
    using var context = GetInMemoryDbContext();
    var repo = new BookRepository(context);

    var book = new Book { Id = 100, Title = "Bad Rating", Author = "Test", Rating = rating, Comments = "OK" };

    await Assert.ThrowsAsync<ArgumentException>(() => repo.Add(book));
  }

  [Fact]
  public async Task Add_Throws_WhenCommentsContainHorrible()
  {
    using var context = GetInMemoryDbContext();
    var repo = new BookRepository(context);

    var book = new Book { Id = 101, Title = "Negative", Author = "Test", Rating = 3, Comments = "This is horrible" };

    await Assert.ThrowsAsync<ArgumentException>(() => repo.Add(book));
  }

  [Fact]
  public async Task Update_UpdatesBook_WhenValid()
  {
    using var context = GetInMemoryDbContext();
    var repo = new BookRepository(context);

    var book = context.Books.First();
    book.Comments = "Updated review";
    await repo.Update(book);

    var updatedBook = context.Books.First(b => b.Id == book.Id);
    Assert.Equal("Updated review", updatedBook.Comments);
  }

  [Fact]
  public async Task Update_Throws_WhenCommentsContainHorrible()
  {
    using var context = GetInMemoryDbContext();
    var repo = new BookRepository(context);

    var book = context.Books.First();
    book.Comments = "horrible book";

    await Assert.ThrowsAsync<InvalidOperationException>(() => repo.Update(book));
  }

}
