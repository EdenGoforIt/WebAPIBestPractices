using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
	public class CompanyDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string FullAddress { get; set; }
		public IEnumerable<EmployeeDto> Employees { get; set; }
	}
}