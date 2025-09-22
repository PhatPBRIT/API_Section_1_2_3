namespace BookStoreApi.Repositories
{
    public interface IBookAuthorsRepository
    {
        bool BookExists(int bookId);
        bool AuthorExists(int authorId);
        bool AddBookAuthor(int bookId, int authorId);
    }
}
