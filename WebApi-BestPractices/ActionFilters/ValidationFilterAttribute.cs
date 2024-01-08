using System.Linq;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi_BestPractices.ActionFilters
{
	public class ValidationFilterAttribute : IActionFilter
	{
		private readonly ILoggerManager _logger;
		public ValidationFilterAttribute(ILoggerManager logger)
		{
			_logger = logger;
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{
			var action = context.RouteData.Values["action"];
			var controller = context.RouteData.Values["controller"];

			var param = context.RouteData.Values.FirstOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

			if (param is null)
			{
				_logger.LogError($"Object sent from the client is null. Controller: {controller}, action: {action}");
				context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
				return;
			}

			if (!context.ModelState.IsValid)
			{
				_logger.LogError($"Invalid model state for the object. Controller: {controller}, action: {action}");
				context.Result = new UnprocessableEntityObjectResult(context.ModelState);
			}

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			throw new System.NotImplementedException();
		}
	}
}