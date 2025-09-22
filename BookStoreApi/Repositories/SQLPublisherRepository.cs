using BookStoreApi.Models.Domain;
using BookStoreApi.Models.DTOs;
using BookStoreApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPI_simple.Data;

namespace BookAPIStore.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;

        public SQLPublisherRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool ExistsByName(string name)
           => _context.Publishers.Any(p => p.Name == name);

        public bool ExistsByNameExcludingId(string name, int excludeId)
            => _context.Publishers.Any(p => p.Name == name && p.Id != excludeId);

        // GET ALL
        public List<PublisherDTO> GetAllPublishers()
        {
            return _context.Publishers
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
        }

        // GET BY ID
        public PublisherNoIdDTO GetPublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null!; // giữ nguyên đúng kiểu trong PDF

            return new PublisherNoIdDTO
            {
                Name = publisher.Name
            };
        }

        // ADD
        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            // tao entity dung class Domain Publisher
            var entity = new Publisher
            {
                Name = addPublisherRequestDTO.Name
            };

            _context.Publishers.Add(entity);
            _context.SaveChanges();

            return new AddPublisherRequestDTO
            {
                Name = entity.Name
            };
        }
        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var entity = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (entity == null) return null!;

            entity.Name = publisherNoIdDTO.Name;
            _context.SaveChanges();

            return new PublisherNoIdDTO
            {
                Name = entity.Name
            };
        }


        public PublisherDTO? DeletePublisherById(int id)
        {
            var entity = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (entity == null) return null;

            _context.Publishers.Remove(entity);
            _context.SaveChanges();

            return new PublisherDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}