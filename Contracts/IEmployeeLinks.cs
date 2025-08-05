using System.Collections.Generic;
using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Contracts;

public interface IEmployeeLinks
{
	LinkResponse TryGenerateLinks(IEnumerable<EmployeeDto> employeesDto,
			string fields, long companyId, HttpContext httpContext);

}