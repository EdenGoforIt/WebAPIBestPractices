using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Entities.Models;
using System.Linq.Dynamic.Core;


namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> Filter(this IQueryable<Employee> employees, uint minAge, uint maxAge)
    {
        return employees.Where(e => e.Age >= minAge && e.Age <= maxAge);
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

    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
    {
        if (string.IsNullOrEmpty(orderByQueryString))
        {
            return employees.OrderBy(x => x.Name);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);

        return employees.OrderBy(orderQuery);
    }
}