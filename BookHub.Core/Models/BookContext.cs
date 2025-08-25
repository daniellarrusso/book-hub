using BookHub.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHub.Core.Repositories;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
}
