using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IBookAuthorsRepository _bookAuthorsRepository;

        public BookAuthorsController(IBookAuthorsRepository bookAuthorsRepository)
        {
            _bookAuthorsRepository = bookAuthorsRepository;
        }

        [HttpPost("add")]
        public IActionResult AddBookAuthor([FromQuery] int bookId, [FromQuery] int authorId)
        {
            // Kiểm tra và thêm bản ghi
            if (!_bookAuthorsRepository.BookExists(bookId))
            {
                ModelState.AddModelError("bookId", $"Book with ID {bookId} does not exist.");
                return BadRequest(ModelState);
            }

            if (!_bookAuthorsRepository.AuthorExists(authorId))
            {
                ModelState.AddModelError("authorId", $"Author with ID {authorId} does not exist.");
                return BadRequest(ModelState);
            }

            var success = _bookAuthorsRepository.AddBookAuthor(bookId, authorId);
            if (success)
            {
                return Ok(new { message = "Book-Author relationship added successfully." });
            }

            return StatusCode(500, "Failed to add Book-Author relationship.");
        }
    }
}