using LazyDataReader;
using NUnit.Framework;

namespace LazyDataReaderTests
{
    public class Tests
    {
        #region Public Methods

        [Test]
        public void IgnoreNamespace()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml");

            Assert.NotNull(data);
        }

        [Test]
        public void TestEncoding()
        {
            var data = Reader.GetFromFile<NeTEx.Light.PublicationDeliveryStructure>(
                path: @"..\..\..\NeTEx\NOR_NOR-Line-8317_134_18-317_Korgen-Laiskardalen.xml",
                classNamespaceUri: "http://www.netex.org.uk/netex");

            Assert.NotNull(data);
        }

        [Test]
        public void TestFileOClassO()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS.xml");

            Assert.NotNull(data);
        }

        [Test]
        public void TestFileOClassW()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS.xml",
                classNamespaceUri: "http://intf.mb.ivu.de/");

            Assert.NotNull(data);
        }

        [Test]
        public void TestFileWClassO()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml",
                classNamespaceUri: "",
                fileNamespaceUri: "http://intf.mb.ivu.de/");

            Assert.NotNull(data);
        }

        [Test]
        public void TestFileWClassW()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml",
                classNamespaceUri: "http://intf.mb.ivu.de/");

            Assert.NotNull(data);
        }

        [Test]
        public void TestFileWClassWOtherNS()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS-OtherNS.xml",
                classNamespaceUri: "http://intf.mb.ivu.de/",
                fileNamespaceUri: "http://abc.de/");

            Assert.NotNull(data);
        }

        #endregion Public Methods
    }
}