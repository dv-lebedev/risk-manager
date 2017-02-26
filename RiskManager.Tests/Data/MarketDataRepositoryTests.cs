
/*
 The MIT License (MIT)
 Copyright (c) 2017 Denis Lebedev
 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:
 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace RiskManager.Data.Tests
{
    [TestClass()]
    public class MarketDataRepositoryTests
    {
        private string ConnectionString
        {
            get
            {
                return @"Server=LEBEDEV\SQLEXPRESS01;Database=marketdatatest;Trusted_Connection=True;";
            }
        }

        [TestMethod()]
        public void GetMainTableTest()
        {
            string[] symbols = { "LKOH", "SBER", "TATN", "ROSN", "GAZP", "MICEX" };

            var repository = new SQLMarketData(ConnectionString);

            var table = repository.GetMainTable();

            Assert.AreEqual(6, table.Count);

            foreach(var item in table)
            {
                if(!symbols.Contains(item.Symbol))
                {
                    System.Diagnostics.Debug.WriteLine(item.Symbol);
                    Assert.Fail();
                }
            }
        }

        [TestMethod()]
        public void GetTest()
        {
            GetTest("GAZP", new DateTime(2016, 1, 1), new DateTime(2016, 9, 8), 134.91, 154.55, 252);
            GetTest("GAZP", new DateTime(2016, 3, 17), new DateTime(2016, 8, 5), 161.2, 146.26, 142);
        }

        private void GetTest(string symbol, DateTime first, DateTime last,
            double firstPrice, double lastPrice, int count)
        {
            var repository = new SQLMarketData(ConnectionString);

            var data = repository.Get("GAZP", first, last);

            Assert.AreEqual(count, data.Count);

            Stock firstStock = data.First();
            Assert.AreEqual(first, firstStock.DateTime);
            Assert.AreEqual(firstPrice, (double)firstStock.Price);

            Stock lastStock = data.Last();
            Assert.AreEqual(last, lastStock.DateTime);
            Assert.AreEqual(lastPrice, (double)lastStock.Price);
        }
    }
}