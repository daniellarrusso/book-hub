using BookHub.Core.Repositories;
using BookHub.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
  private readonly IBookRepository _bookRepository;

  public BooksController(IBookRepository bookRepository)
  {
    _bookRepository = bookRepository;
  }

  /// <summary>
  /// Get a List of Books
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  public async Task<ActionResult<PagedResult<Book>>> GetBooks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null)
  {
    if (page <= 0 || pageSize <= 0)
      return BadRequest("page and pageSize must be greater than zero.");

    var result = await _bookRepository.GetBooks(page, pageSize, search, sort);
    return Ok(result);
  }
}