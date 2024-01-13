namespace Entities.RequestFeatures;

public abstract class RequestParameters
{
	const int maxPageSize = 50;
	public int PageNumber { get; set; } = 1;
	private int _pageSize = 10;

	public int PageSize
	{
		get => _pageSize;
		set => _pageSize = value > maxPageSize ? maxPageSize : value;
	}

	public uint MinAge { get; set; }
	public uint MaxAge { get; set; }
	public bool ValidAge => MaxAge > MinAge;
	public string SearchTerm { get; set; }
}

public class EmployeeParameters : RequestParameters { }
