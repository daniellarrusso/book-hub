using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookHub.Core.Repositories
{
  public class BookContextFactory : IDesignTimeDbContextFactory<BookContext>
  {
    public BookContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<BookContext>();

      // Use your real connection string here
      optionsBuilder.UseSqlite("Data Source=bookhub.db");

      return new BookContext(optionsBuilder.Options);
    }
  }
}
