using HandyTool.Stock.StockInfo;
using HandyTool.Stock.Tests.Dummies;

using NUnit.Framework;

using System.Linq;

namespace HandyTool.Stock.Tests.Unit
{
    [TestFixture]
    public class StockTests
    {
        [Test]
        public void Stock_Instantiated_WithDefaultConstructor()
        {
            //arrange

            //act

            IStock dummyStock = new DummyStock();

            //assert

            Assert.That(dummyStock, Is.Not.Null);
            Assert.That(dummyStock.Name, Is.Null);
            Assert.That(dummyStock.Tag, Is.Null);
            Assert.That(dummyStock.SourceUrl, Is.Null);
        }

        [Test]
        public void Stock_Instantiated_WithCustomConstructor()
        {
            //arrange

            var name = "dummy name";
            var service = "dummy service";
            var tag = "dummy tag";
            var sourceUrl = "dummy url";

            //act

            IStock dummyStock = new DummyStock(name, service, tag, sourceUrl);

            //assert

            Assert.That(dummyStock, Is.Not.Null);
            Assert.That(dummyStock.Name, Is.EqualTo(name));
            Assert.That(dummyStock.Service, Is.EqualTo(service));
            Assert.That(dummyStock.Tag, Is.EqualTo(tag));
            Assert.That(dummyStock.SourceUrl, Is.EqualTo(sourceUrl));
        }

        [Test]
        public void Stocks_GetStockList_ListNotNull()
        {
            //arrange

            var stocks = new StockInfoLoader();

            //act

            var stockList = stocks.GetStockList();

            //assert

            Assert.That(stockList, Is.Not.Null);
        }

        [Test]
        public void StockInfoLoader_Returns_StockList()
        {
            //arrange

            //act

            var stockList = new StockInfoLoader().GetStockList().ToList();

            //assert

            Assert.That(stockList, Is.Not.Null);
            Assert.That(stockList.Count, Is.GreaterThan(0));
        }

        [TestCase("Yahoo")]
        [TestCase("Garanti")]
        public void YahooStock_GetStockData(string serviceName)
        {
            //arrange

            StockData stockData = new StockData();

            var stockList = new StockInfoLoader().GetStockList();
            var stockService = StockServiceFactory.CreateService(serviceName);

            //act

            stockService.StockUpdated += (sender, args) => { stockData = args.StockData; };
            stockService.GetStockData(stockList.First(s => s.Service.Equals(serviceName) && s.Tag.Equals("EURTRY")));

            //assert

            Assert.That(stockData, Is.Not.Null);
        }

        [Test]
        public void StockData_Instantiate_NotNull()
        {
            //arrange

            var stockData = new StockData();

            //act

            stockData.ActualData = 0.1;
            stockData.ChangeRate = 0.2;

            //assert

            Assert.That(stockData.ActualData, Is.EqualTo(0.1));
            Assert.That(stockData.ChangeRate, Is.EqualTo(0.2));
        }
    }
}
