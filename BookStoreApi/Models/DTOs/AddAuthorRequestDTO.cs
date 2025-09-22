using System.ComponentModel.DataAnnotations;
namespace BookStoreApi.Models.DTOs
{
    public class AddAuthorRequestDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        public string FullName { set; get; }
    }
}
