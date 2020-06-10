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
            var data = Reader.GetData<StandardTrafficNetworkWithoutRoot.TrafficNetworkWithoutRoot>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WithNS.xml",
                rootElement: "trafficNetwork");

            Assert.NotNull(data);
        }

        [Test]
        public void TestWithRoot()
        {
            var data = Reader.GetData<StandardTrafficNetworkWithRoot.TrafficNetworkWithRoot>(
                path: @"..\..\..\TrafficNetwork\StandardTrafficNetworkExport_WithNS.xml");

            Assert.NotNull(data);
        }

        #endregion Public Methods
    }
}