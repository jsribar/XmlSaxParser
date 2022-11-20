using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSaxParser
{
    public class XmlElement
    {
        public XmlElement(string name)
        {
            Name = name;
        }

        public void AddChild(XmlElement child)
        {
            children.Add(child);
        }

        public void AddAttribute(string name, string value)
        {
            attributes.Add(name, value);
        }

        public string Name { get; private set; }
        public IEnumerable<XmlElement> Children => children;

        private List<XmlElement> children = new List<XmlElement>();
        private Dictionary<string, string> attributes = new Dictionary<string, string>();
    }
}
