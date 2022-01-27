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
            var data1 = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml");

            Assert.True(data1.networkPointAreas.Length > 0);

            var data2 = Reader.GetFromFile<RailML2.RailML>(
                path: @"..\..\..\railML\Example_Import.xml",
                classNamespace: "http://www.railml.org/schemas/2013");

            Assert.False(string.IsNullOrWhiteSpace(data2.Rollingstock.Vehicles[0].Id));

            var data3 = Reader.GetFromFile<RailML2.RailML>(
                path: @"..\..\..\railML\Example_Tracks.xml",
                classNamespace: "http://www.railml.org/schemas/2013");

            Assert.False(string.IsNullOrWhiteSpace(data3.Infrastructure.OperationControlPoints[0].Code));
        }

        [Test]
        public void TestClassOAttrNS()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS-Attr.xml",
                classNamespace: "http://intf.mb.ivu.de/");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber1));
        }

        [Test]
        public void TestEncoding()
        {
            var data = Reader.GetFromFile<NeTEx.Light.PublicationDeliveryStructure>(
                path: @"..\..\..\NeTEx\NOR_NOR-Line-8317_134_18-317_Korgen-Laiskardalen.xml",
                classNamespace: "http://www.netex.org.uk/netex");

            Assert.True(data.DataObjects.CompositeFrame.Length > 0);
        }

        [Test]
        public void TestFileOClassO()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS.xml");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Test]
        public void TestFileOClassW()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS.xml",
                classNamespace: "http://intf.mb.ivu.de/");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Test]
        public void TestFileWClassO()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Test]
        public void TestFileWClassW()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml",
                classNamespace: "http://intf.mb.ivu.de/");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Test]
        public void TestFileWClassWAttrNS()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS-AttrNS.xml",
                classNamespace: "http://intf.mb.ivu.de/",
                acceptedNamespaces: "http://test.de/");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber1));
            Assert.True(data.networkPointAreas[0].validity.fromDate.Year == 2019);
        }

        [Test]
        public void TestFileWClassWOtherNS()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS-OtherNS.xml",
                classNamespace: "http://intf.mb.ivu.de/",
                acceptedNamespaces: new string[] { "http://test.de/" });

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
            Assert.True(data.networkPointAreas[0].validity.fromDate.Year == 2019);
        }

        #endregion Public Methods
    }
}