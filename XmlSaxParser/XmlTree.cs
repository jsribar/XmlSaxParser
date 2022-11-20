using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSaxParser
{
    public class XmlTree
    {
        public XmlElement Root => root;

        private XmlElement root = null;

        // Stack holds current hierarchy of elements. Last element corresponds to the active element and this stack is used to identify parent for a new element.
        private Stack<XmlElement> elementStack = new Stack<XmlElement>();

        public void ElementStartHandler(object sender, ElementStartEventArgs args)
        {
            var newElement = new XmlElement(args.Name);
            // If element stack is empty, this element is the root.
            if (elementStack.Count == 0)
            {
                root = newElement;
            }
            // If new element is not the root element then add it as a child of current active element, which is the top element on the stack.
            else
            {
                elementStack.Peek().AddChild(newElement);
            }
            // Push element to the element stack.
            elementStack.Push(newElement);
        }

        public void ElementEndtHandler(object sender, ElementEndEventArgs args)
        {
            if (elementStack.Count == 0)
            {
                throw new Exception("Invalid XML document");
            }
            var elementRemoved = elementStack.Pop();
            if (elementRemoved.Name != args.Name)
            {
                throw new Exception("Closing tag name is different from opening tag name");
            }
        }
    }
}
