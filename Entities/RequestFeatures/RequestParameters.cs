namespace Entities.RequestFeatures;

public abstract class RequestParameters
{
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string SearchTerm { get; set; }

    public string OrderBy { get; set; }

    public string Fields { get; set; }
}