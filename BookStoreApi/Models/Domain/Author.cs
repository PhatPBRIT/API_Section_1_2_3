using System.ComponentModel.DataAnnotations;
using WebAPI_simple.Models.Domain;
namespace BookStoreApi.Models.Domain
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } = default!;

        public List<Book_Author> BookAuthors { get; set; } = new List<Book_Author>();
    }
}
