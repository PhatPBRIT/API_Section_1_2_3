using BookStoreApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStoreApi.Filters
{
    public class ValidateAuthorCanDeleteAttribute : ActionFilterAttribute
    {
        private readonly IBookAuthorsRepository _repository;

        public ValidateAuthorCanDeleteAttribute(IBookAuthorsRepository repository)
        {
            _repository = repository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out var value) && value is int authorId)
            {
                if (_repository.ExistsByAuthorId(authorId))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        message = "Hãy gỡ liên kết trong Book_Author trước khi xóa."
                    });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
