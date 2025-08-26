using BookHub.Models;

namespace BookHub.Core.Repositories;

public interface IBookRepository
{
    Task<PagedResult<Book>> GetBooks(int page, int pageSize, string? search, string? sort);

    Task<Book> GetById(int id);

    Task<Book> Add(Book book);

    Task Update(Book book);

    Task Delete(int id);
}
