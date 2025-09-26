using BookStoreApi.Models.DTOs;
using WebAPI_simple.Models.Domain;

namespace BookStoreApi.Repositories
{
    public interface IBookAuthorsRepository
    {
        AddBookAuthorRequestDTO AddBook_Author(AddBookAuthorRequestDTO addBook_AuthorRequestDTO);
        public bool ExistsByBookId(int bookId);
        public bool ExistsByAuthorId(int authorId);
        bool Exists(int bookId, int authorId);
    }
}
