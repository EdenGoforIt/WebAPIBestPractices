namespace Entities.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public EmployeeParameters()
    {
        OrderBy = "name";
    }

    public uint MinAge { get; set; }
    public uint MaxAge { get; set; }
    public bool ValidAge => MaxAge > MinAge;
}