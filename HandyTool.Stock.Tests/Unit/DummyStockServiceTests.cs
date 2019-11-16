using HandyTool.Stock.Tests.Dummies;
using NUnit.Framework;

namespace HandyTool.Stock.Tests.Unit
{
    [TestFixture]
    public class DummyStockServiceTests
    {
        [TestCase]
        public void StockService_Instantiated_NotNull()
        {
            //arrange

            //act

            IStockService stockService = new DummyStockService();

            //assert

            Assert.That(stockService, Is.Not.Null);
        }

        [TestCase]
        public void StockService_GetStockData_IsCalled_AnEventFired()
        {
            //arrange

            bool isEventFired = false;

            IStockService stockService = new DummyStockService();
            stockService.StockUpdated += (sender, args) => { isEventFired = true; };

            //act

            stockService.GetStockData(null);

            //assert

            Assert.That(isEventFired, Is.True);
        }

        [TestCase]
        public void StockService_GetStockData_IsNotCalled_NoEventFired()
        {
            //arrange

            bool isEventFired = false;

            IStockService stockService = new DummyStockService();
            stockService.StockUpdated += (sender, args) => { isEventFired = true; };

            //act

            //assert

            Assert.That(isEventFired, Is.False);
        }

        [TestCase]
        public void StockService_GetStockData_IsCalled_EventArgsGathered()
        {
            //arrange

            StockUpdateEventArgs stockEventArgs = null;

            IStockService stockService = new DummyStockService();
            stockService.StockUpdated += (sender, args) =>
            {
                stockEventArgs = args;
            };

            //act

            stockService.GetStockData(null);

            //assert

            Assert.That(stockEventArgs, Is.Not.Null);
        }

        [TestCase]
        public void StockService_GetStockData_IsNotCalled_NoEventArgsGathered()
        {
            //arrange

            StockUpdateEventArgs stockEventArgs = null;

            IStockService stockService = new DummyStockService();
            stockService.StockUpdated += (sender, args) =>
            {
                stockEventArgs = args;
            };

            //act

            //assert

            Assert.That(stockEventArgs, Is.Null);
        }

        [TestCase]
        public void StockService_GetStockData_IsCalled_StockDataGathered_FromEventArgs()
        {
            //arrange

            StockUpdateEventArgs stockEventArgs = null;

            IStockService stockService = new DummyStockService();
            stockService.StockUpdated += (sender, args) =>
            {
                stockEventArgs = args;
            };

            //act

            stockService.GetStockData(null);

            //assert

            Assert.That(stockEventArgs, Is.Not.Null);
            Assert.That(stockEventArgs.StockData, Is.Not.Null);
            Assert.That(stockEventArgs.StockData.ActualData, Is.EqualTo(0.1));
            Assert.That(stockEventArgs.StockData.ChangeRate, Is.EqualTo(0.2));
        }
    }
}
