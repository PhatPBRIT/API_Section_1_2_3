using WebAPI_simple.Models.Domain;

namespace BookStoreApi.Models.Domain
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
