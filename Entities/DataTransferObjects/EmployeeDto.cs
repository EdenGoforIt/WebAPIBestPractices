using System;

namespace Entities.DataTransferObjects
{
	public class EmployeeDto
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string Position { get; set; }
	}
}