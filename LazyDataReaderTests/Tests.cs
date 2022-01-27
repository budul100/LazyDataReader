using LazyDataReader;
using Xunit;

namespace LazyDataReaderTests
{
    public class Tests
    {
        #region Public Methods

        [Fact]
        public void IgnoreNamespace()
        {
            var data1 = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml");

            Assert.True(data1.networkPointAreas.Length > 0);

            var data2 = Reader.GetFromFile<RailML2.RailML>(
                path: @"..\..\..\railML\Example_Import.xml",
                namespaceUri: "http://www.railml.org/schemas/2013");

            Assert.True(data2.Infrastructure.OperationControlPoints.Length > 0);

            var data3 = Reader.GetFromFile<RailML2.RailML>(
                path: @"..\..\..\railML\Example_Tracks.xml",
                namespaceUri: "http://www.railml.org/schemas/2013");

            Assert.True(data3.Infrastructure.OperationControlPoints.Length > 0);
        }

        [Fact]
        public void TestEncoding()
        {
            var data = Reader.GetFromFile<NeTEx.Light.PublicationDeliveryStructure>(
                path: @"..\..\..\NeTEx\NOR_NOR-Line-8317_134_18-317_Korgen-Laiskardalen.xml",
                namespaceUri: "http://www.netex.org.uk/netex");

            Assert.True(data.DataObjects.CompositeFrame.Length > 0);
        }

        [Fact]
        public void TestFileOClassO()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS.xml");

            Assert.True(data.networkPointAreas.Length > 0);
        }

        [Fact]
        public void TestFileOClassW()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS.xml",
                namespaceUri: "http://intf.mb.ivu.de/");

            Assert.True(data.networkPointAreas.Length > 0);
        }

        [Fact]
        public void TestFileWClassO()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml");

            Assert.True(data.networkPointAreas.Length > 0);
        }

        [Fact]
        public void TestFileWClassW()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml",
                namespaceUri: "http://intf.mb.ivu.de/");

            Assert.True(data.networkPointAreas.Length > 0);
        }

        [Fact]
        public void TestFileWClassWOtherNS()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS-OtherNS.xml",
                namespaceUri: "http://intf.mb.ivu.de/");

            Assert.True(data.networkPointAreas.Length > 0);
        }

        #endregion Public Methods
    }
}