
using BookStoreApi.Models.Domain;
using BookStoreApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public BooksController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET http://localhost:port/api/get-all-books
        [HttpGet("get-all-books")]
        public IActionResult GetAll()
        {
            // Get Data from Database - Domain Model
            var allBooksDomain = _dbContext.Books;

            // Map domain models to DTOs
            var allBooksDTO = allBooksDomain.Select(Book => new BookWithAuthorAndPublisherDTO()
            {
                Id = Book.Id,
                Title = Book.Title,
                Description = Book.Description,
                IsRead = Book.IsRead,
                DateRead = Book.IsRead ? Book.DateRead.Value : null,
                Rate = Book.IsRead ? Book.Rate.Value : null,
                Genre = Book.Genre,
                CoverUrl = Book.CoverUrl,
                PublisherName = Book.Publisher.Name,
                AuthorNames = Book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).ToList();

            // return DTOs
            return Ok(allBooksDTO);
        }
        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            // Get book Domain model from Db
            var bookWithDomain = _dbContext.Books.Where(n => n.Id == id);

            if (bookWithDomain == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTOs
            var bookWithIdDTO = bookWithDomain.Select(book => new BookWithAuthorAndPublisherDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            });

            return Ok(bookWithIdDTO);
        }
        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] addBookRequestDTO addBookRequestDTO)
        {
            // Map DTO to Domain Model
#pragma warning disable CS8601 // Possible null reference assignment.
            var bookDomainModel = new Book
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                IsRead = addBookRequestDTO.IsRead,
                DateRead = addBookRequestDTO.DateRead,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = addBookRequestDTO.DateAdded,
                PublisherID = addBookRequestDTO.PublisherID
            };
#pragma warning restore CS8601 // Possible null reference assignment.

            // Use Domain Model to create Book
            _dbContext.Books.Add(bookDomainModel);
            _dbContext.SaveChanges();

            foreach (var id in addBookRequestDTO.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = bookDomainModel.Id,
                    AuthorId = id
                };

                _dbContext.Books_Authors.Add(_book_author);
            }
            _dbContext.SaveChanges();

            return Ok();
        }
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] addBookRequestDTO bookDTO)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDomain != null)
            {
                bookDomain.Title = bookDTO.Title;
                bookDomain.Description = bookDTO.Description;
                bookDomain.IsRead = bookDTO.IsRead;
                bookDomain.DateRead = bookDTO.DateRead;
                bookDomain.Rate = bookDTO.Rate;
                bookDomain.Genre = bookDTO.Genre;
                bookDomain.CoverUrl = bookDTO.CoverUrl;
                bookDomain.DateAdded = bookDTO.DateAdded;
                bookDomain.PublisherID = bookDTO.PublisherID;

                _dbContext.SaveChanges();
            }

            var authorDomain = _dbContext.Books_Authors.Where(a => a.BookId == id).ToList();
            if (authorDomain != null)
            {
                _dbContext.Books_Authors.RemoveRange(authorDomain);
                _dbContext.SaveChanges();
            }

            foreach (var authorid in bookDTO.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = id,
                    AuthorId = authorid
                };
                _dbContext.Books_Authors.Add(_book_author);
            }
            _dbContext.SaveChanges();

            return Ok(bookDTO);
        }
        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var bookDoamin = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDoamin != null)
            {
                _dbContext.Books.Remove(bookDoamin);
                _dbContext.SaveChanges();
            }
            return Ok();
        }
    }
}