using System.Net;
using Contracts;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace WebApi_BestPractices.Extensions;

public static class ExceptionMiddlewareExtendsions
{
	public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
	{
		app.UseExceptionHandler(appError =>
		{
			appError.Run(async context =>
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";

				var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

				if (contextFeature is not null)
				{
					logger.LogError($"Something went wrong: {contextFeature.Error}");
					await context.Response.WriteAsync(new ErrorDetail(context.Response.StatusCode, "Internal Server Errror")
					 .ToString());
				}
			});
		});
	}

}