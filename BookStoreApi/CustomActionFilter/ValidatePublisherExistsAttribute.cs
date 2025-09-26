using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BookStoreApi.Models.DTOs;
using BookStoreApi.Repositories;
namespace BookStoreApi.CustomActionFilter
{
    public class ValidatePublisherExistsAttribute : ActionFilterAttribute
    {
        private readonly IPublisherRepository _publisherRepository;

        public ValidatePublisherExistsAttribute(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("addBookRequestDTO"))
            {
                var dto = context.ActionArguments["addBookRequestDTO"] as addBookRequestDTO;

                if (dto != null && !_publisherRepository.ExistsById(dto.PublisherID))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        error = $"Publisher with ID {dto.PublisherID} does not exists."
                    });
                }
            }
        }
    }
}
