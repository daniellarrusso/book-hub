namespace BookHub.Core.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooks();
}
