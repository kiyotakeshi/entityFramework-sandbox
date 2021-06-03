using System.Collections.Generic;
using System.Threading.Tasks;
using sandbox.models;

namespace entityFramework_sandbox.services
{
  public interface IReviewRepository
  {
    Task<bool> ReviewExists(int reviewId);
    Task<Review> GetReview(int reviewId);
    Task<ICollection<Review>> GetReviews();

    Task<ICollection<Review>> GetReviewsOfABook(int bookId);
    Task<Book> GetBookOfAReview(int reviewId);
  }
}
