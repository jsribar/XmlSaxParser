using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlSaxParser
{
    public class SaxParser
    {
        public delegate void ElementStartEventHandler(object sender, ElementStartEventArgs args);
        public delegate void ElementEndEventHandler(object sender, ElementEndEventArgs args);

        public event ElementStartEventHandler ElementStart;
        public event ElementEndEventHandler ElementEnd;

        public SaxParser()
        {
            settings.Async = true;
        }

        XmlReaderSettings settings = new XmlReaderSettings();

        public async Task Parse(TextReader textReader)
        {
            using (XmlReader reader = XmlReader.Create(textReader, settings))
            {
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            OnElementStart(reader.Name);
                            break;
                        case XmlNodeType.EndElement:
                            OnElementEnd(reader.Name);
                            break;
                        default:
                            //Console.WriteLine("Other node {0} with value {1}",
                            //                reader.NodeType, reader.Value);
                            break;
                    }
                }
            }
        }

        protected virtual void OnElementStart(string name)
        {
            ElementStart?.Invoke(this, new ElementStartEventArgs(name));
        }
        protected virtual void OnElementEnd(string name)
        {
            ElementEnd?.Invoke(this, new ElementEndEventArgs(name));
        }
    }
}
