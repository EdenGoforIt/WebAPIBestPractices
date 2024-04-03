using System.Collections.Generic;
using System.Xml;
using Entities.LinkModels;

namespace Entities.Models;

public class Entity
{
    private void WriteLinksToXml(string key, object value, XmlWriter writer)
    {
        writer.WriteStartElement(key);
        if (value.GetType() == typeof(List<Link>))
        {
            foreach (var val in (List<Link>)value)
            {
                writer.WriteStartElement(nameof(Link));
                WriteLinksToXml(nameof(val.Href), val.Href, writer);
                WriteLinksToXml(nameof(val.Method), val.Method, writer);
                WriteLinksToXml(nameof(val.Rel), val.Rel, writer);
                writer.WriteEndElement();
            }
        }
        else
        {
            writer.WriteString(value.ToString());
        }

        writer.WriteEndElement();
    }
}