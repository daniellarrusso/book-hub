using BookHub.Models;

namespace BookHub.Core.Repositories;

public interface IBookRepository
{
    Task<PagedResult<Book>> GetBooks(int page, int pageSize, string? search, string? sort);
}
