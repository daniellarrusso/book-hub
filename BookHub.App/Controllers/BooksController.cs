using BookHub.Core.Repositories;
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
  public async Task<IActionResult> GetBooks()
  {
    var books = await _bookRepository.GetBooks();
    return Ok(books);
  }
}