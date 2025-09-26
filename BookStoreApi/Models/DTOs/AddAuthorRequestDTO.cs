using System.ComponentModel.DataAnnotations;
namespace BookStoreApi.Models.DTOs
{
    public class AddAuthorRequestDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        public string FullName { set; get; }
        public object BookId { get; internal set; }
        public object AuthorId { get; internal set; }
    }
}
