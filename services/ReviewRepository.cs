using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sandbox.models;

namespace entityFramework_sandbox.services
{
  public class ReviewRepository : IReviewRepository
  {
    private readonly DataContext _context;
    public ReviewRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<Book> GetBookOfAReview(int reviewId)
    {
      var bookId = await _context.Reviews.Where(r => r.Id == reviewId).Select(b => b.Id).FirstOrDefaultAsync();
      return await _context.Books.Where(b => b.Id == bookId).FirstOrDefaultAsync();
    }

    public async Task<Review> GetReview(int reviewId)
    {
      return await _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
    }

    public async Task<ICollection<Review>> GetReviews()
    {
      return await _context.Reviews.OrderBy(r => r.Rating).ToListAsync();
    }

    public async Task<ICollection<Review>> GetReviewsOfABook(int bookId)
    {
      return await _context.Reviews.Where(b => b.Book.Id == bookId).ToListAsync();
    }

    public async Task<bool> ReviewExists(int reviewId)
    {
      return await _context.Reviews.AnyAsync(r => r.Id == reviewId);
    }
  }
}
