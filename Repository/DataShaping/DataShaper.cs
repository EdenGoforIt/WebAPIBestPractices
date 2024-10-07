using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Contracts;
using Entities.Models;

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
        var requiredProperties = GetRequiredProperties(fieldsString);
        return FetchDataForEntity(entity, requiredProperties);
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
        var shapedData = new List<ExpandoObject>();

        foreach (var entity in entities)
        {
            var shapedObject = FetchDataForEntity(entity, requiredProperties);
            shapedData.Add(shapedObject);
        }

        return shapedData;
    }

    private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
    {
        var shapedObject = new ExpandoObject();

        foreach (var property in requiredProperties)
        {
            var objectPropertyValue = property.GetValue(entity);
            shapedObject.TryAdd(property.Name, objectPropertyValue);
        }

        return shapedObject;
    }

    // private ShapedEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo>
    //     requiredProperties)
    // {
    //     var shapedObject = new ShapedEntity();
    //     foreach (var property in requiredProperties)
    //     {
    //         var objectPropertyValue = property.GetValue(entity);
    //         shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
    //     }
    //     var objectProperty = entity.GetType().GetProperty("Id");
    //     shapedObject.Id = (Guid)objectProperty.GetValue(entity);
    //     return shapedObject;
    // }
}