using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using XmlSaxParser;

namespace UnitTests
{
    [TestClass]
    public class TestXmlTree
    {
        SaxParser parser = new SaxParser();
        XmlTree tree = null;

        // This method is performed before each test so there is no need to rewrite these statements for each test method.
        [TestInitialize]
        public void Initialize()
        {
            tree = new XmlTree();
            parser.XmlElementStart += tree.ElementStartHandler;
            parser.XmlElementEnd += tree.ElementEndHandler;
            parser.XmlText += tree.TextHandler;
        }

        [TestMethod]
        public void XmlTreeContainsNoElementForEmptyXml()
        {
            using (TextReader reader = new StringReader(""))
            {
                var result = parser.Parse(reader);
            }

            Assert.IsNull(tree.Root);
        }

        [TestMethod]
        public void XmlTreeContainsRootElementOnlyForAnXmlWithSingleElement()
        {
            using (TextReader reader = new StringReader("<document/>"))
            {
                var result = parser.Parse(reader);
            }

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual("document", tree.Root.Name);
        }

        [TestMethod]
        public void XmlTreeContainsRootAndTwoSiblingChildren()
        {
            using (TextReader reader = new StringReader("<document><child1></child1><child2></child2></document>"))
            {
                var result = parser.Parse(reader);
            }

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual("document", tree.Root.Name);
            
            Assert.AreEqual(2, tree.Root.Children.Count());
            var child1 = tree.Root.Children.ElementAt(0);
            Assert.AreEqual(NodeType.Element, child1.NodeType);
            Assert.AreEqual("child1", ((XmlElement)tree.Root.Children.ElementAt(0)).Name);

            var child2 = tree.Root.Children.ElementAt(1);
            Assert.AreEqual(NodeType.Element, child2.NodeType);
            Assert.AreEqual("child2", ((XmlElement)tree.Root.Children.ElementAt(1)).Name);
        }

        [TestMethod]
        public void XmlTreeContainsRootWithChildAndGrandChild()
        {
            using (TextReader reader = new StringReader("<document><child><grandchild/></child></document>"))
            {
                var result = parser.Parse(reader);
            }

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual("document", tree.Root.Name);
            
            Assert.AreEqual(1, tree.Root.Children.Count());
            var child = tree.Root.Children.ElementAt(0);
            Assert.AreEqual(NodeType.Element, child.NodeType);
            Assert.AreEqual("child", ((XmlElement)child).Name);
            
            Assert.AreEqual(1, child.Children.Count());
            var grandchild = child.Children.ElementAt(0);
            Assert.AreEqual(NodeType.Element, grandchild.NodeType);
            Assert.AreEqual("grandchild", ((XmlElement)child.Children.ElementAt(0)).Name);
        }

        [TestMethod]
        public void XmlTreeContainsSingleTextNode()
        {
            using (TextReader reader = new StringReader("<document>text</document>"))
            {
                var result = parser.Parse(reader);
            }

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual("document", tree.Root.Name);
            
            Assert.AreEqual(1, tree.Root.Children.Count());
            var child = tree.Root.Children.ElementAt(0);
            Assert.AreEqual(NodeType.Text, child.NodeType);
            Assert.AreEqual("text", ((XmlText)child).Text);
        }

        [TestMethod]
        public void XmlTreeRootContainsMultipleTextAndElementNodes()
        {
            using (TextReader reader = new StringReader("<document>text0<child>text1</child>text2</document>"))
            {
                var result = parser.Parse(reader);
            }

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual("document", tree.Root.Name);
            
            Assert.AreEqual(3, tree.Root.Children.Count());

            var child0 = tree.Root.Children.ElementAt(0);
            Assert.AreEqual(NodeType.Text, child0.NodeType);
            Assert.AreEqual("text0", ((XmlText)child0).Text);

            var child1 = tree.Root.Children.ElementAt(1);
            Assert.AreEqual(NodeType.Element, child1.NodeType);
            Assert.AreEqual("child", ((XmlElement)child1).Name);

            Assert.AreEqual(1, child1.Children.Count());
            var child2 = child1.Children.ElementAt(0);
            Assert.AreEqual(NodeType.Text, child2.NodeType);
            Assert.AreEqual("text1", ((XmlText)child2).Text);

            var child3 = tree.Root.Children.ElementAt(2);
            Assert.AreEqual(NodeType.Text, child3.NodeType);
            Assert.AreEqual("text2", ((XmlText)child3).Text);
        }
    }
}
