using HandyTool.Stock.StockInfo;
using HandyTool.Stock.Tests.Dummies;

using NUnit.Framework;

using System.Linq;

namespace HandyTool.Stock.Tests.Unit
{
    [TestFixture]
    public class StockTests
    {
        [TestCase]
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

        [TestCase]
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

        [TestCase]
        public void Stocks_GetStockList_ListNotNull()
        {
            //arrange

            var stocks = new StockInfoLoader();

            //act

            var stockList = stocks.GetStockList();

            //assert

            Assert.That(stockList, Is.Not.Null);
        }

        [TestCase]
        public void YahooStock_GetStockData()
        {
            //arrange

            StockData stockData = new StockData();

            var stockList = new StockInfoLoader().GetStockList();
            var yahooStock = StockServiceFactory.CreateService(StockServiceType.Yahoo);

            //act

            yahooStock.StockUpdated += (sender, args) => { stockData = args.StockData; };
            yahooStock.GetStockData(stockList.First(s => s.Service.Equals("Yahoo") && s.Tag.Equals("EURTRY")));

            //assert

            Assert.That(stockData, Is.Not.Null);
        }

        [TestCase]
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
