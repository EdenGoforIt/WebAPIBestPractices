using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Contracts;

namespace Repository.DataShaping;

public class DataShaper<T> : IDataShaper<T> where T : class
{
    private PropertyInfo[] Properties { get; set; } =
        typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

    public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
    {
        var requiredProperties = GetRequiredProperties(fieldsString);

        return FetchData(entities, requiredProperties);
    }

    public ExpandoObject ShapeData(T entity, string fieldsString)
    {
        throw new System.NotImplementedException();
    }

    private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
    {
        var requiredProperties = new List<PropertyInfo>();

        if (!string.IsNullOrEmpty(fieldsString))
        {
            var fields = fieldsString.Split(", ", StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fields)
            {
                var property = Properties.FirstOrDefault(x =>
                    x.Name.Equals(field, StringComparison.InvariantCultureIgnoreCase));

                if (property is null)
                {
                    continue;
                }

                requiredProperties.Add(property);
            }
        }
        else
        {
            requiredProperties = Properties.ToList();
        }

        return requiredProperties;
    }

    private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
    {
        
    }
}