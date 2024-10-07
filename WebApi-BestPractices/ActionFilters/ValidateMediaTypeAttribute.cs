using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi_BestPractices.ActionFilters;

public class ValidateMediaTypeAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var acceptHeaderPresent = context.HttpContext.Request.Headers.ContainsKey("Accept");

        if (!acceptHeaderPresent)
        {
            context.Result = new BadRequestObjectResult($"Accept header is missing");
            return;
        }

        var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

        if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue outMediaType))
        {
            context.Result =
                new BadRequestObjectResult(
                    $"Media type not present. Please add Accept Header with the required media type");
            return;
        }

        context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new System.NotImplementedException();
    }
}