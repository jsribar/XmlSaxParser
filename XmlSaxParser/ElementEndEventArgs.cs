using System;

namespace XmlSaxParser
{
    public class ElementEndEventArgs : EventArgs
    {
        public ElementEndEventArgs(string name)
        {
            Name = name;
        }

        public readonly string Name;
    }
}
