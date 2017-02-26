using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace RiskManager.Data.Tests
{
    [TestClass()]
    public class CSVMarketDataTests
    {
        [TestMethod()]
        public void GetTest()
        {
            GetTest("GAZP", new DateTime(2016, 1, 4), new DateTime(2016, 12, 30), 134.91, 154.55, 252);
            GetTest("GAZP", new DateTime(2016, 2, 4), new DateTime(2016, 8, 5), 135.07, 135.5, 127);
        }

        private void GetTest(string symbol, DateTime first, DateTime last,
            double firstPrice, double lastPrice, int count)
        {
            var repository = new CSVMarketData("csv-market-data-2016");

            var data = repository.Get("GAZP", first, last);

            Assert.AreEqual(count, data.Count);

            Stock firstStock = data.First();
            Assert.AreEqual(first, firstStock.DateTime);
            Assert.AreEqual(firstPrice, (double)firstStock.Price);

            Stock lastStock = data.Last();
            Assert.AreEqual(last, lastStock.DateTime);
            Assert.AreEqual(lastPrice, (double)lastStock.Price);
        }

        [TestMethod()]
        public void GetMainTableTest()
        {
            string[] symbols = { "LKOH", "SBER", "TATN", "ROSN", "GAZP", "MICEX" };

            var repository = new CSVMarketData("csv-market-data-2016");

            var table = repository.GetMainTable();

            Assert.AreEqual(6, table.Count);

            foreach (var item in table)
            {
                if (!symbols.Contains(item.Symbol))
                {
                    System.Diagnostics.Debug.WriteLine(item.Symbol);
                    Assert.Fail();
                }
            }
        }
    }
}