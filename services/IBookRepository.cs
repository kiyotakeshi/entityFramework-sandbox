using System.Collections.Generic;
using System.Threading.Tasks;
using sandbox.models;

namespace entityFramework_sandbox.services
{
  public interface IBookRepository
  {
    Task<bool> BookExists(int bookId);
    Task<Book> GetBook(int bookId);
    Task<ICollection<Book>> GetBooks();
  }
}
