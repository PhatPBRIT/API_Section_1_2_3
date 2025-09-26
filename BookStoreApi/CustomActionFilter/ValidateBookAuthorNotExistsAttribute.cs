using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BookStoreApi.Models.DTOs;
using BookStoreApi.Repositories;
namespace BookStoreApi.CustomActionFilter
{
    public class ValidateBookAuthorNotExistsAttribute : ActionFilterAttribute
    {
        private readonly IBookAuthorsRepository _repository;

        public ValidateBookAuthorNotExistsAttribute(IBookAuthorsRepository repository)
        {
            _repository = repository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("addBook_AuthorRequestDTO", out var value) && value is AddBookAuthorRequestDTO dto)
            {
                if (_repository.Exists(dto.BookId, dto.AuthorId))
                {
                    context.Result = new ConflictObjectResult(new
                    {
                        message = $"The relationship BookID={dto.BookId} and AuthorID={dto.AuthorId} already exists."
                    });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
