using System.Collections.Generic;
using System.Threading.Tasks;
using entityFramework_sandbox.services;
using Microsoft.AspNetCore.Mvc;
using sandbox.models;

namespace entityFramework_sandbox.Controllers
{
  public class BooksController : BaseApiController
  {
    private readonly IBookRepository _bookRepository;
    public BooksController(IBookRepository bookRepository)
    {
      _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Book>>> GetBooks()
    {
      var books = await _bookRepository.GetBooks();
      return Ok(books);
    }

    [HttpGet("{bookId}")]
    public async Task<ActionResult<Book>> GetBook(int bookId)
    {
      if(!await _bookRepository.BookExists(bookId))
        return NotFound();

      var book = await _bookRepository.GetBook(bookId);
      return Ok(book);
    }

  }
}
