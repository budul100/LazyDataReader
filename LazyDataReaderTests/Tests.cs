using System.Linq;
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
                classNamespace: "http://www.railml.org/schemas/2013");

            Assert.False(string.IsNullOrWhiteSpace(data2.Rollingstock.Vehicles[0].Id));

            var data3 = Reader.GetFromFile<RailML2.RailML>(
                path: @"..\..\..\railML\Example_Tracks.xml",
                classNamespace: "http://www.railml.org/schemas/2013");

            Assert.False(string.IsNullOrWhiteSpace(data3.Infrastructure.OperationControlPoints[0].Code));
        }

        [Fact]
        public void RemoveAttributes()
        {
            var data = Reader.GetFromFile<RailML2.RailML>(
                path: @"..\..\..\railML\Example_UnknownNamespace.xml",
                classNamespace: "http://www.railml.org/schemas/2013",
                additionalNamespaces: default,
                removalPatterns: @"xsi\:type\=\""rhb[^\""]*\""");

            Assert.True(data.Infrastructure.OperationControlPoints.Length > 0);
        }

        [Fact]
        public void RemoveNamespaces()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS-Prefix.xml",
                removalPatterns: "ns1\\:");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Fact]
        public void ReplaceCommaInNumbers()
        {
            var data = Reader.GetFromFile<RINF.RINFData>(
                path: @"..\..\..\RINF\Example_RINF.xml",
                replaceCommaInNumbers: true);

            Assert.Equal(2, data.OperationalPoint.Length);
            Assert.True(data.SectionOfLine.Single().SOLLength.Value == (decimal)2.834);
        }

        [Fact]
        public void SetEncoding()
        {
            var data = Reader.GetFromFile<RailML2.RailML>(
                path: @"..\..\..\railML\Example_Abbreviations.xml",
                encoding: System.Text.Encoding.UTF8,
                classNamespace: "http://www.railml.org/schemas/2013");

            Assert.Equal("2°VA", data.Infrastructure.OperationControlPoints[6].Abbrevation);
        }

        [Fact]
        public void TestClassOAttrNS()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS-Attr.xml",
                classNamespace: "http://intf.mb.ivu.de/");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber1));
        }

        [Fact]
        public void TestFileOClassO()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS.xml");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Fact]
        public void TestFileOClassW()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_ONS.xml",
                classNamespace: "http://intf.mb.ivu.de/");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Fact]
        public void TestFileWClassO()
        {
            var data = Reader.GetFromFile<TrafficNetworkONS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Fact]
        public void TestFileWClassW()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS.xml",
                classNamespace: "http://intf.mb.ivu.de/");

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
        }

        [Fact]
        public void TestFileWClassWAttrNS()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS-AttrNS.xml",
                classNamespace: "http://intf.mb.ivu.de/",
                additionalNamespaces: ["http://test.de/"]);

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber1));
            Assert.Equal(2019, data.networkPointAreas[0].validity.fromDate.Year);
        }

        [Fact]
        public void TestFileWClassWOtherNS()
        {
            var data = Reader.GetFromFile<TrafficNetworkWNS.TrafficNetwork>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WNS-OtherNS.xml",
                classNamespace: "http://intf.mb.ivu.de/",
                additionalNamespaces: ["http://test.de/"]);

            Assert.False(string.IsNullOrWhiteSpace(data.networkPointAreas[0].networkPointAreaKey.externalNumber));
            Assert.Equal(2019, data.networkPointAreas[0].validity.fromDate.Year);
        }

        [Fact]
        public void UnknownEncoding()
        {
            var data = Reader.GetFromFile<NeTEx.Light.PublicationDeliveryStructure>(
                path: @"..\..\..\NeTEx\NOR_NOR-Line-8317_134_18-317_Korgen-Laiskardalen.xml",
                classNamespace: "http://www.netex.org.uk/netex",
                additionalNamespaces: ["http://www.opengis.net/gml/3.2"]);

            Assert.True(data.DataObjects.CompositeFrame.Length > 0);
        }

        #endregion Public Methods
    }
}