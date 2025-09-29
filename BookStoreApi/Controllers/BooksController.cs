﻿using BookStoreApi.CustomActionFilter;
using BookStoreApi.Filters;
using BookStoreApi.Models.DTOs;
using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPI_simple.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BooksController> _logger;
        public BooksController(AppDbContext dbContext, IBookRepository bookRepository,ILogger<BooksController> logger)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
            _logger = logger;
        }

        //get all books
        // GET: /api/Books/get-all-books?filterOn=Name&filterQuery=Track
        [HttpGet("get-all-books")]
        [Authorize(Roles = "Read")]
        public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string?
       filterQuery,
        [FromQuery] string? sortBy, [FromQuery] bool isAscending,
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            _logger.LogInformation("GetAll Book Action method was invoked");
            _logger.LogWarning("This is a warning log");
            _logger.LogError("This is a error log");
            // su dung reposity pattern 
            var allBooks = _bookRepository.GetAllBooks(filterOn, filterQuery, sortBy,
           isAscending, pageNumber, pageSize);
            //debug
            _logger.LogInformation($"Finished GetAllBook request with data {JsonSerializer.Serialize(allBooks)}");
            return Ok(allBooks);
        }

        [HttpGet]
        [Route("get-book-by-id/{id}")]
        [Authorize(Roles = "Read")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = _bookRepository.GetBookById(id);
            if (bookWithIdDTO == null)
            {
                return NotFound();
            }
            return Ok(bookWithIdDTO);
        }

        [HttpPost("add-book")]
        [ValidateModelAttribute]
        [Authorize(Roles = "Write")]
        public IActionResult AddBook([FromBody] addBookRequestDTO addBookRequestDTO)
        {
            var publisherExists = _dbContext.Publishers.Any(p => p.Id == addBookRequestDTO.PublisherID);
            if (!publisherExists)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.PublisherID),
                    "PublisherID không tồn tại trong bảng Publishers.");
                return BadRequest(ModelState);
            }

            if (ValidateAddBook(addBookRequestDTO))
            {
                var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
                return Ok(bookAdd);
            }
             return BadRequest(ModelState);
        }
        [HttpPost("and-book")]
        [Authorize(Roles = "Read,Write")]
        public IActionResult AddBookNew([FromBody] addBookRequestDTO addBookRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
                return Ok(bookAdd);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("update-book-by-id/{id}")]
        [Authorize(Roles = "Read,Write")]
        public IActionResult UpdateBookById([FromRoute] int id, [FromBody] addBookRequestDTO bookDTO)
        {
            var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
            if (updateBook == null)
            {
                return NotFound();
            }
            return Ok(updateBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        [Authorize(Roles = "Read,Write")]
        public IActionResult DeleteBookById([FromRoute] int id)
        {
            var deleteBook = _bookRepository.DeleteBookById(id);
            if (deleteBook == null)
            {
                return NotFound();
            }
            return Ok(deleteBook);
        }
        #region Private methods
        private bool ValidateAddBook(addBookRequestDTO addBookRequestDTO)
        {
            if (addBookRequestDTO == null)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO), $"Please add book data");
                return false;
            }
            // kiem tra Description NotNull
            if (string.IsNullOrEmpty(addBookRequestDTO.Description))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Description),
               $"{nameof(addBookRequestDTO.Description)} cannot be null");
            }
            // kiem tra rating (0,5) 
            if (addBookRequestDTO.Rate < 0 || addBookRequestDTO.Rate > 5)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Rate),
               $"{nameof(addBookRequestDTO.Rate)} cannot be less than 0 and more than 5");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        
    }
}
#endregion