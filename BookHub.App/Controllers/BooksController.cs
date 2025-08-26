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
  /// Get a paginated response of books
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

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var book = await _bookRepository.GetById(id);
    return book is null ? NotFound() : Ok(book);
  }

  [HttpPost]
  public async Task<IActionResult> Add(Book book)
  {
    try
    {
      var created = await _bookRepository.Add(book);
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, Book book)
  {
    if (id != book.Id) return BadRequest("Book must have a ID");
    await _bookRepository.Update(book);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    await _bookRepository.Delete(id);
    return NoContent();
  }
}