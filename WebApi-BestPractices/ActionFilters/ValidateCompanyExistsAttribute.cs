using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi_BestPractices.ActionFilters
{
	/// <summary>
	///  Action filter executes before and after an action method executes. Action filter attributes can be applied to an individual action method or to a controller. When an action filter is applied to a controller, 
	///  it will be applied to all the controller's action methods. 
	/// </summary>
	public class ValidateCompanyExistsAttribute : IAsyncActionFilter
	{
		private readonly IRepositoryManager _repository;
		private readonly ILoggerManager _logger;
		public ValidateCompanyExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
		{
			_logger = logger;
			_repository = repository;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
			var id = (long)context.ActionArguments["id"];
			var company = await _repository.Company.GetCompany(id, trackChanges);

			if (company is null)
			{
				_logger.LogInfo($"Comapny with id: {id} doesn't exist in the database");
				context.Result = new NotFoundResult();
			}
			else
			{
				context.HttpContext.Items.Add("company", company);
				await next();
			}
		}
	}

}