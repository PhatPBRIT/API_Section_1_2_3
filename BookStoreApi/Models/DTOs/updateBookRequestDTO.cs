using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models.DTOs
{
    public record updateBookRequestDTO(
         [Required, MinLength(2)] string Title,
         int? Year,
         decimal? Price,
         int? PublisherId,
         List<int>? AuthorIds
     );
}
