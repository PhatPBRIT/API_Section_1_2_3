using System.ComponentModel.DataAnnotations;
using BookStoreApi.Models.Domain;

namespace WebAPI_simple.Models.Domain
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; } = null!;
        public string CoverUrl { get; set; } = null!;
        public DateTime DateAdded { get; set; }

        // Navigation Properties – One publisher has many books
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; } = null!;

        // Navigation Properties – One book has many book_author
        public List<Book_Author> Book_Authors { get; set; } = new List<Book_Author>();

    }
}
