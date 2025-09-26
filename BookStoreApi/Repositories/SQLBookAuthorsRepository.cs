using BookStoreApi.Models.DTOs;
using BookStoreApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;

namespace BookStoreApi.Repositories
{
    public class BookAuthorsRepository : IBookAuthorsRepository
    {
        private readonly AppDbContext _dbContext;
        public BookAuthorsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AddBookAuthorRequestDTO AddBook_Author(AddBookAuthorRequestDTO addBook_AuthorRequestDTO)
        {
            var bookAuthorDomain = new Book_Author
            {
                BookId = addBook_AuthorRequestDTO.BookId,
                AuthorId = addBook_AuthorRequestDTO.AuthorId,
            };
            _dbContext.Books_Authors.Add(bookAuthorDomain);
            _dbContext.SaveChanges();
            return addBook_AuthorRequestDTO;
        }
        public bool ExistsByBookId(int bookId)
        {
            return _dbContext.Books.Any(b => b.Id == bookId);
        }
        public bool ExistsByAuthorId(int authorId)
        {
            return _dbContext.Authors.Any(a => a.Id == authorId);
        }

        public bool Exists(int bookId, int authorId)
        {
            throw new NotImplementedException();
        }
    }
}