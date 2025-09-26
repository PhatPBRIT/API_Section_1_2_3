using BookStoreApi.CustomActionFilter;
using BookStoreApi.Filters;
using BookStoreApi.Models.DTOs;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.Data;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Book_AuthorController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IBookAuthorsRepository _book_AuthorRepository;

        public Book_AuthorController(AppDbContext dbContext, IBookAuthorsRepository book_AuthorRepository)
        {
            _dbContext = dbContext;
            _book_AuthorRepository = book_AuthorRepository;

        }
        [HttpPost("add-book_author")]
        [ValidateModel]
        public IActionResult AddBook_Author([FromBody] AddBookAuthorRequestDTO addBook_AuthorRequestDTO)
        {
            if (ValidateAddBook_Author(addBook_AuthorRequestDTO))
            {
                var book_authorAdd = _book_AuthorRepository.AddBook_Author(addBook_AuthorRequestDTO);
                return Ok(book_authorAdd);
            }
            else return BadRequest(ModelState);
        }
        private bool ValidateAddBook_Author(AddBookAuthorRequestDTO addBook_AuthorRequestDTO)
        {
            if (!_book_AuthorRepository.ExistsByBookId(addBook_AuthorRequestDTO.BookId))
            {
                ModelState.AddModelError(nameof(addBook_AuthorRequestDTO), $"BookId does not exist in Books table");
            }

            if (!_book_AuthorRepository.ExistsByAuthorId(addBook_AuthorRequestDTO.AuthorId))
            {
                ModelState.AddModelError(nameof(addBook_AuthorRequestDTO), $"AuthorId does not exist in Authors table");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
    }
}