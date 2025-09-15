using BookStoreApi.Models.Domain;

namespace WebAPI_simple.Models.Domain
{
    public class Book_Author
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        // Navigation Property – One book has many book_author
        public Book Book { get; set; } = null!;

        public int AuthorId { get; set; }
        // Navigation Property – One author has many book_author
        public Author Author { get; set; } = null!;
    }
}
