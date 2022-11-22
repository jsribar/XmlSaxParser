using System.Collections.Generic;

namespace XmlSaxParser
{
    public class XmlElement : XmlNode
    {
        public XmlElement(string name) : base(NodeType.Element)
        {
            Name = name;
        }

        public void AddAttribute(string name, string value)
        {
            attributes.Add(name, value);
        }

        public string Name { get; private set; }

        private Dictionary<string, string> attributes = new Dictionary<string, string>();
    }
}
