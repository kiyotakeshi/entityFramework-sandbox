using System.Collections.Generic;
using System.Threading.Tasks;
using entityFramework_sandbox.services;
using Microsoft.AspNetCore.Mvc;
using sandbox.models;

namespace entityFramework_sandbox.Controllers
{
  public class AuthorsController : BaseApiController
  {
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    public AuthorsController(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
      _bookRepository = bookRepository;
      _authorRepository = authorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Author>>> GetAuthors()
    {
      var authors = await _authorRepository.GetAuthors();
      // return Ok(authors);
      return Ok(authors);
    }

    [HttpGet("{authorId}")]
    public async Task<ActionResult<Author>> GetAuthor(int authorId)
    {
      if (!await _authorRepository.AuthorExists(authorId))
        return NotFound();

      var book = await _authorRepository.GetAuthor(authorId);
      return Ok(book);
    }

    [HttpGet("{authorId}/books")]
    public async Task<ActionResult<ICollection<Book>>> GetBooksByAuthor(int authorId)
    {
      if (!await _authorRepository.AuthorExists(authorId))
        return NotFound();

      var books = await _authorRepository.GetBooksByAuthor(authorId);
      return Ok(books);
    }

    [HttpGet("books/{bookId}")]
    public async Task<ActionResult<ICollection<Author>>> GetAuthorsOfABook(int bookId)
    {
      if (!await _bookRepository.BookExists(bookId))
        return NotFound();

      var authors = await _authorRepository.GetAuthorsOfABook(bookId);

      return Ok(authors);
    }
  }
}
