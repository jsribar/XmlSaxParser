namespace XmlSaxParser
{
    public class XmlTextEventArgs
    {
        public XmlTextEventArgs(string text)
        {
            Text = text;
        }

        public readonly string Text;
    }
}
