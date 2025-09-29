using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}

