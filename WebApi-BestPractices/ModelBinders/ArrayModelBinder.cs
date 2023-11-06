using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi_BestPractices.ModelBinders
{
	public class ArrayModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (!bindingContext.ModelMetadata.IsEnumerableType)
			{
				bindingContext.Result = ModelBindingResult.Failed();
				return Task.CompletedTask;
			}

			var providedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

			if (string.IsNullOrEmpty(providedValue))
			{
				bindingContext.Result = ModelBindingResult.Success(null);
				return Task.CompletedTask;
			}

			var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];

			var converter = TypeDescriptor.GetConverter(genericType);

			var objectArray = providedValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => converter.ConvertFromString(x.Trim())).ToArray();

			var array = Array.CreateInstance(genericType, objectArray.Length);

			objectArray.CopyTo(array, 0);
			bindingContext.Model = array;

			bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);

			return Task.CompletedTask;
		}
	}
}