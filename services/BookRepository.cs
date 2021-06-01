using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sandbox.models;

namespace entityFramework_sandbox.services
{
  public class BookRepository : IBookRepository
  {
    private readonly DataContext _context;
    public BookRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<bool> BookExists(int bookId)
    {
      return await _context.Books.AnyAsync(b => b.Id == bookId);
    }

    public async Task<Book> GetBook(int bookId)
    {
      return await _context.Books.Where(b => b.Id == bookId).FirstOrDefaultAsync();
    }

    public async Task<ICollection<Book>> GetBooks()
    {
      return await _context.Books.OrderBy(b => b.Title).ToListAsync();
    }
  }
}
