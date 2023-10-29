using System.Text.Json;

namespace Entities.ErrorModel;

public record ErrorDetail(int StatusCode, string Message)
{
	public override string ToString()
	{
		return JsonSerializer.Serialize(this);
	}
}