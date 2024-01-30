using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Repository.Extensions;

public static class OrderQueryBuilder
{
	public static string CreateOrderQuery<T>(string orderByQueryString) where T : class
	{
		var orderParams = orderByQueryString.Trim().Split(",");
		var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
		var orderQueryBuilder = new StringBuilder();
		foreach (var param in orderParams)
		{
			if (string.IsNullOrEmpty(param))
			{
				continue;
			}

			var propertyFromQueryName = param.Split(" ")[0];
			var objectProperty = propertyInfos.FirstOrDefault(x =>
				x.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
			if (objectProperty is null)
			{
				continue;
			}

			var direction = param.EndsWith(" desc") ? "descending" : "ascending";
			orderQueryBuilder.Append($"{objectProperty.Name} {direction}");
		}

		var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

		return orderQuery;
	}

}