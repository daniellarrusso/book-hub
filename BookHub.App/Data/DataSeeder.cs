using BookHub.Core.Repositories;
using BookHub.Models;

namespace BookHub.Data;

public static class DataSeeder
{
  public static void Seed(BookContext context)
  {
    if (context.Books.Any()) return; // Already seeded

    var books = new List<Book>();

    for (int i = 1; i <= 25; i++)
    {
      books.Add(new Book
      {
        Title = $"Sample Book {i}",
        Author = $"Author {i}",
        ISBN = $"{1000 + i}",
        Rating = i % 5 + 1, // 1 to 5
        Comments = $"This is comment for book {i}",
        NoteStatus = (i % 3 == 0) ? "Reviewed" : "Draft",
        CoverImageUrl = $"https://picsum.photos/seed/{i}"
      });
    }

    context.Books.AddRange(books);
    context.SaveChanges();
  }
}
