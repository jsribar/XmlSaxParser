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
            parser.ElementStart += tree.ElementStartHandler;
            parser.ElementEnd += tree.ElementEndtHandler;
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
            Assert.AreEqual("child1", tree.Root.Children.ElementAt(0).Name);
            Assert.AreEqual("child2", tree.Root.Children.ElementAt(1).Name);
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
            Assert.AreEqual("child", child.Name);
            
            Assert.AreEqual(1, child.Children.Count());
            Assert.AreEqual("grandchild", child.Children.ElementAt(0).Name);
        }
    }
}
