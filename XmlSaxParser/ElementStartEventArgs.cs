using System;

namespace XmlSaxParser
{
    public class ElementStartEventArgs : EventArgs
    {
        public ElementStartEventArgs(string name)
        {
            Name = name;
        }

        public readonly string Name;
    }
}
