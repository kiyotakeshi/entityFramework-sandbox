using System.Collections.Generic;
using System.Threading.Tasks;
using sandbox.models;

namespace entityFramework_sandbox.services
{
  public interface IAuthorRepository
  {
    Task<bool> AuthorExists(int authorId);
    Task<Author> GetAuthor(int authorId);
    Task<ICollection<Author>> GetAuthors();
    Task<ICollection<Author>> GetAuthorsOfABook(int bookId);
    Task<ICollection<Book>> GetBooksByAuthor(int authorId);
  }
}
