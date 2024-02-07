using System.Collections.Generic;
using System.Dynamic;

namespace Contracts;

public interface IDataShaper<in T> where T : class
{
    IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
    ExpandoObject ShapeData(T entity, string fieldsString);
}