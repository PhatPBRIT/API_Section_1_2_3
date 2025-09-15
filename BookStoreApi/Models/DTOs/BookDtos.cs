namespace BookStoreApi.Models.DTOs
{
    public class BookDtos
    {
        public record BookCreateDto(
            string Title, 
            int? Year, 
            decimal? Price, 
            int? PublisherId, 
            List<int>? AuthorIds
        );
        public record BookUpdateDto(string Title, int? Year, decimal? Price, int? PublisherId, List<int>? AuthorIds);
        public record BookReadDto(int Id, string Title, int? Year, decimal? Price, string? PublisherName, List<string> Authors);
    }
}
