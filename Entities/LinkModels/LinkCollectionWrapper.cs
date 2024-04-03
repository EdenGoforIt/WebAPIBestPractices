using System.Collections.Generic;

namespace Entities.LinkModels;

public class LinkCollectionWrapper<T> : LinkResourceBase
{
    public LinkCollectionWrapper()
    {
    }

    public LinkCollectionWrapper(List<T> value)
    {
        Value = value;
    }

    public List<T> Value { get; set; } = new();
}