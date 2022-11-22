using System.Collections.Generic;

namespace XmlSaxParser
{
    public enum NodeType
    {
        Element,
        Text
    }

    public abstract class XmlNode
    {
        public readonly NodeType NodeType;

        public XmlNode(NodeType nodeType)
        {
            NodeType = nodeType;
        }

        public virtual void AddChild(XmlNode child)
        {
            children.Add(child);
        }

        public IEnumerable<XmlNode> Children => children;

        private List<XmlNode> children = new List<XmlNode>();
    }
}
