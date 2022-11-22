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
        public delegate void XmlElementStartEventHandler(object sender, XmlElementStartEventArgs args);
        public delegate void XmlElementEndEventHandler(object sender, XmlElementEndEventArgs args);
        public delegate void XmlTextEventHandler(object sender, XmlTextEventArgs args);

        public event XmlElementStartEventHandler XmlElementStart;
        public event XmlElementEndEventHandler XmlElementEnd;
        public event XmlTextEventHandler XmlText;

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
                        case XmlNodeType.Text:
                            OnText(await reader.GetValueAsync());
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
            XmlElementStart?.Invoke(this, new XmlElementStartEventArgs(name));
        }
        protected virtual void OnElementEnd(string name)
        {
            XmlElementEnd?.Invoke(this, new XmlElementEndEventArgs(name));
        }
        protected virtual void OnText(string text)
        {
            XmlText?.Invoke(this, new XmlTextEventArgs(text));
        }
    }
}
