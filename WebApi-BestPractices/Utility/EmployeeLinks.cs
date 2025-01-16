using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using Contracts;

using Entities.DataTransferObjects;
using Entities.LinkModels;
using System.Linq;
using Entities.Models;
using Repository;

namespace WebApi_BestPractices.Utility;
public class EmployeeLinks
{
	private readonly LinkGenerator _linkGenerator;
	private readonly IDataShaper<EmployeeDto> _dataShaper;

	public EmployeeLinks(LinkGenerator linkGenerator, IDataShaper<EmployeeDto> dataShaper)
	{
		_linkGenerator = linkGenerator;
		_dataShaper = dataShaper;
	}

	public LinkResponse TryGenerateLinks(IEnumerable<EmployeeDto> employeesDto, string fields, Guid companyId, HttpContext httpContext)
	{
		var shapedEmployees = ShapeData(employeesDto, fields);

		if (ShouldGenerateLinks(httpContext))
		{

			return ReturnLinkedEmployees(employeesDto, fields, companyId, httpContext, shapedEmployees);
		}

		return ReturnShapedEmployees(shapedEmployees);
	}

	private List<Entity> ShapeData(IEnumerable<EmployeeDto> employeesDto, string fields)
	{
		return _dataShaper.ShapeData(employeesDto, fields)
											.Select(e => e.Entity)
											.ToList();
	}

	private LinkResponse ReturnShapedEmployees(IEnumerable<Entity> shapedEmployees)
	{
		throw new NotImplementedException();
	}

	private LinkResponse ReturnLinkedEmployees(IEnumerable<EmployeeDto> employeesDto, string fields, Guid companyId, HttpContext httpContext, IEnumerable<Entity> shapedEmployees)
	{
		throw new NotImplementedException();
	}

	private static bool ShouldGenerateLinks(HttpContext httpContext)
	{
		var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

		return mediaType.MediaType.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
	}
}