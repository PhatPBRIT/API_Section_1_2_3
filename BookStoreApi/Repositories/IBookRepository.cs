using BookStoreApi.Models.DTOs;
using BookStoreApi.Models.Domain;
using WebAPI_simple.Models.Domain;
namespace BookStoreApi.Repositories
{
    public interface IBookRepository
    {
        List<BookWithAuthorAndPublisherDTO> GetAllBooks();
        BookWithAuthorAndPublisherDTO GetBookById(int id);
        addBookRequestDTO AddBook(addBookRequestDTO addBookRequestDTO);
        addBookRequestDTO? UpdateBookById(int id, addBookRequestDTO bookDTO);
        Book? DeleteBookById(int id);
    }
}
