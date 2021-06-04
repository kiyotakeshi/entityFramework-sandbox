using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sandbox.models;

namespace entityFramework_sandbox.services
{
  public class ReviewerRepository : IReviewerRepository
  {
    private readonly DataContext _context;
    public ReviewerRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<Reviewer> GetReviewer(int reviewerId)
    {
      return await _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefaultAsync();
    }

    public async Task<ICollection<Reviewer>> GetReviewers()
    {
      return await _context.Reviewers.OrderBy(r => r.FirstName).ToListAsync();
    }

    public async Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId)
    {
      // 発行される SQL のイメージ
      // select * from reviews where reviews.ReviewerId = 3
      return await _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToListAsync();
    }

    public async Task<Reviewer> GetReviwerOfAReview(int reviewId)
    {
      // 発行される SQL のイメージ
      // select Reviewers.Id, Reviewers.FirstName, Reviewers.LastName from Reviews
      //     join Reviewers on Reviews.ReviewerId = Reviewers.Id
      //     WHERE Reviews.ReviewerId = 1
      var reviewerId = await _context.Reviews.Where(r => r.Id == reviewId).Select(r => r.Reviewer.Id).FirstOrDefaultAsync();
      return await _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefaultAsync();
    }

    public async Task<bool> ReviewerExists(int reviewerId)
    {
      return await _context.Reviewers.AnyAsync(r => r.Id == reviewerId);
    }
  }
}
