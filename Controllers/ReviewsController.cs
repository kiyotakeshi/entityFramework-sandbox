using System.Collections.Generic;
using System.Threading.Tasks;
using entityFramework_sandbox.services;
using Microsoft.AspNetCore.Mvc;
using sandbox.models;

namespace entityFramework_sandbox.Controllers
{
  public class ReviewsController : BaseApiController
  {
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;
    public ReviewsController(IReviewRepository reviewRepository, IBookRepository bookRepository)
    {
      _reviewRepository = reviewRepository;
      _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Author>>> GetReviews()
    {
      var reviews = await _reviewRepository.GetReviews();
      // return Ok(reviews);
      return Ok(reviews);
    }

    [HttpGet("{reviewId}")]
    public async Task<ActionResult<Author>> GetReview(int reviewId)
    {
      if (!await _reviewRepository.ReviewExists(reviewId))
        return NotFound();

      var review = await _reviewRepository.GetReview(reviewId);
      return Ok(review);
    }

    [HttpGet("books/{bookId}")]
    public async Task<ActionResult<ICollection<Author>>> GetReviewsOfABook(int bookId)
    {
      if (!await _bookRepository.BookExists(bookId))
        return NotFound();

      var reviews = await _reviewRepository.GetReviewsOfABook(bookId);

      return Ok(reviews);
    }

    [HttpGet("{reviewId}/book")]
    public async Task<ActionResult<ICollection<Book>>> GetBookOfAReview(int reviewId)
    {
      if (!await _reviewRepository.ReviewExists(reviewId))
        return NotFound();

      var books = await _reviewRepository.GetBookOfAReview(reviewId);
      return Ok(books);
    }
  }
}
