using BookHub.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHub.Core.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookContext _context;
    private const int MaxBooks = 25;

    public BookRepository(BookContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Book>> GetBooks(int page, int pageSize, string? search, string? sort)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        IQueryable<Book> query = _context.Books.AsNoTracking();

        // Search by title or author (case-insensitive)
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(b =>
                b.Title.ToLower().Contains(term) ||
                b.Author.ToLower().Contains(term));
        }

        // Sort by title
        query = sort?.ToLower() switch
        {
            "desc" => query.OrderByDescending(b => b.Title),
            _ => query.OrderBy(b => b.Title) // default asc
        };

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Book>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Book?> GetById(int id) =>
      await _context.Books.FindAsync(id);

    public async Task<Book> Add(Book book)
    {
        if (_context.Books.Count() >= MaxBooks)
            throw new InvalidOperationException("Maximum of 25 books reached.");

        if (book.Rating is < 1 or > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        if (book.Comments.Contains("horrible", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Comments cannot contain the word 'horrible'.");

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task Update(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null) return;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }


}
