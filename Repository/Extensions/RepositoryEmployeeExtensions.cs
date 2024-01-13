using System.Linq;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
	public static IQueryable<Employee> Filter(this IQueryable<Employee> employees, uint minAge, uint maxAge)
	{
		return employees.Where(e => e.Age >= minAge && e.Age <= maxAge)
	}

	public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
	{
		if (string.IsNullOrEmpty(searchTerm))
		{
			return employees;
		}

		var lowerCaseTerm = searchTerm.Trim().ToLower();

		return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
	}
}