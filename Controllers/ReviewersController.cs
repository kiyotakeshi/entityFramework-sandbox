using System.Collections.Generic;
using System.Threading.Tasks;
using entityFramework_sandbox.services;
using Microsoft.AspNetCore.Mvc;
using sandbox.models;

namespace entityFramework_sandbox.Controllers
{
  public class ReviewersController : BaseApiController
  {
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IReviewRepository _reviewRepository;
    public ReviewersController(IReviewerRepository reviewerRepository, IReviewRepository reviewRepository)
    {
      _reviewerRepository = reviewerRepository;
      _reviewRepository = reviewRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Reviewer>>> GetReviewers()
    {
      var reviewers = await _reviewerRepository.GetReviewers();
      return Ok(reviewers);
    }

    [HttpGet("{reviewerId}")]
    public async Task<ActionResult<Reviewer>> GetReviewer(int reviewerId)
    {
      if (!await _reviewerRepository.ReviewerExists(reviewerId))
        return NotFound();

      var reviewer = await _reviewerRepository.GetReviewer(reviewerId);
      return Ok(reviewer);
    }

    [HttpGet("{reviwerId}/reviews")]
    public async Task<ActionResult<ICollection<Review>>> GetReviewsByReviewer(int reviewerId)
    {
      if (!await _reviewerRepository.ReviewerExists(reviewerId))
        return NotFound();

      var reviews = await _reviewerRepository.GetReviewsByReviewer(reviewerId);
      return Ok(reviews);
    }

    // ReviewsController にある方が適切??
    [HttpGet("{reviewId}/reviewer")]
    public async Task<ActionResult<Reviewer>> GetReviewerOfAReview(int reviewId)
    {
      if (!await _reviewRepository.ReviewExists(reviewId))
        return NotFound();

      var reviewer = await _reviewerRepository.GetReviwerOfAReview(reviewId);
      return Ok(reviewer);
    }
  }
}
