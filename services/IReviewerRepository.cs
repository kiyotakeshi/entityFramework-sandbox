using System.Collections.Generic;
using System.Threading.Tasks;
using sandbox.models;

namespace entityFramework_sandbox.services
{
  public interface IReviewerRepository
  {
    Task<bool> ReviewerExists(int reviewerId);
    Task<Reviewer> GetReviewer(int reviewerId);
    Task<ICollection<Reviewer>> GetReviewers();
    Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId);
    Task<Reviewer> GetReviwerOfAReview(int reviewId);
  }
}
