using LazyDataReader;
using NUnit.Framework;

namespace LazyDataReaderTests
{
    public class Tests
    {
        #region Public Methods

        [Test]
        public void TestWithoutRoot()
        {
            var data = Reader.GetFromFile<StandardTrafficNetworkWithoutRoot.TrafficNetworkWithoutRoot>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WithNS.xml",
                classNamespaceUri: "");

            Assert.NotNull(data);
        }

        [Test]
        public void TestWithoutRootAndReplace()
        {
            var data = Reader.GetFromFile<StandardTrafficNetworkWithoutRoot.TrafficNetworkWithoutRoot>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WithNS.xml",
                classNamespaceUri: "",
                fileNamespaceUri: "http://intf.mb.ivu.de/");

            Assert.NotNull(data);
        }

        [Test]
        public void TestWithRoot()
        {
            var data = Reader.GetFromFile<StandardTrafficNetworkWithRoot.TrafficNetworkWithRoot>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WithNS.xml",
                classNamespaceUri: "http://intf.mb.ivu.de/");

            Assert.NotNull(data);
        }

        #endregion Public Methods
    }
}