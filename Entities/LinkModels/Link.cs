namespace Entities.LinkModels;

public class Link
{
    /// <summary>
    /// URl that the client can follow to access a related resource 
    /// </summary>
    public string Href { get; set; }

    /// <summary>
    /// The relationships with the linked resource relative to the current resource
    /// </summary>
    public string Rel { get; set; }

    /// <summary>
    /// Method to use to access URL
    /// </summary>
    public string Method { get; set; }

    public Link()
    {
    }

    public Link(string href, string rel, string method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}