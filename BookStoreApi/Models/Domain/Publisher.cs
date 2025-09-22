using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebAPI_simple.Models.Domain;

namespace BookStoreApi.Models.Domain
{
    [Index(nameof(Name), IsUnique = true)]
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
