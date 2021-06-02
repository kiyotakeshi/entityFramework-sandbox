using System.Collections.Generic;

// https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/concepts/linq/introduction-to-linq-queries
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sandbox.models;

namespace entityFramework_sandbox.services
{
  public class AuthorRepository : IAuthorRepository
  {
    private readonly DataContext _context;
    public AuthorRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<bool> AuthorExists(int authorId)
    {
      return await _context.Authors.AnyAsync(a => a.Id == authorId);
    }

    public async Task<Author> GetAuthor(int authorId)
    {
      return await _context.Authors.Where(a => a.Id == authorId).FirstOrDefaultAsync();
    }

    public async Task<ICollection<Author>> GetAuthors()
    {
      return await _context.Authors.OrderBy(a => a.LastName).ToListAsync();
    }

    public async Task<ICollection<Author>> GetAuthorsOfABook(int bookId)
    {
      return await _context.BookAuthors.Where(b => b.BookId == bookId).Select(a => a.Author).ToListAsync();
    }

    public async Task<ICollection<Book>> GetBooksByAuthor(int authorId)
    {
      return await _context.BookAuthors.Where(a => a.AuthorId == authorId).Select(b => b.Book).ToListAsync();
    }
  }
}
