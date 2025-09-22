using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models.DTOs
{
    public class AddPublisherRequestDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { set; get; }
    }
}
