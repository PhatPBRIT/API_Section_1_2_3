using BookStoreApi.Models.Domain;
using BookStoreApi.Models.DTOs;

namespace BookStoreApi.Repositories
{
    public interface IPublisherRepository
    {
        List<PublisherDTO> GetAllPublishers();
        PublisherNoIdDTO GetPublisherById(int id);
        AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
        PublisherDTO? DeletePublisherById(int id);
        bool ExistsByName(string name);
        bool ExistsById(int id);
        bool ExistsByNameExcludingId(string name, int excludeId);
    }
}
