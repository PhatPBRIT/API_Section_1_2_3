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

        public BookAuthorsRepository(AppDbContext  dbContext)
        {
            _dbContext = dbContext;
        }

        public bool BookExists(int bookId)
            => _dbContext.Books.Any(b => b.Id == bookId);

        public bool AuthorExists(int authorId)
            => _dbContext.Authors.Any(a => a.Id == authorId);

        public bool AddBookAuthor(int bookId, int authorId)
        {
            if (!BookExists(bookId))
            {
                return false; 
            }

            if (!AuthorExists(authorId))
            {
                return false; 
            }

            var bookAuthor = new BookAuthor
            {
                BookId = bookId,
                AuthorId = authorId
            };

            _dbContext.Books_Authors.Add(bookAuthor);
            _dbContext.SaveChanges();
            return true;
        }
    }
}