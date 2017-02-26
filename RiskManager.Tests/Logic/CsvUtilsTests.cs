
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
using System.Linq;

namespace RiskManager.Logic.Tests
{
    [TestClass()]
    public class CsvUtilsTests
    {
        private const string HISTORICAL_PRICES_PATH = "historical-prices";

        [TestMethod()]
        public void ReadAllDataFromTest()
        {
            var values = CsvUtils.ReadAllDataFrom(HISTORICAL_PRICES_PATH, 4);

            Assert.AreEqual(22, values.Count);

            var symbols = values.Keys;

            Assert.AreEqual(true, symbols.Contains("AAPL"));
            Assert.AreEqual(true, symbols.Contains("BP"));
            Assert.AreEqual(true, symbols.Contains("JPM"));
        }

        [TestMethod()]
        public void ReadTest()
        {
            double[] prices = CsvUtils.Read(HISTORICAL_PRICES_PATH + "/AAL.txt", 4);

            double first = prices[0];
            double last = prices[prices.Length - 1];

            Assert.AreEqual(25.360001, first, 0);
            Assert.AreEqual(42.830002, last, 0);
        }

        [TestMethod()]
        public void GetFilesNamesTest()
        {
            var filesNames = CsvUtils.GetFilesNames(HISTORICAL_PRICES_PATH);
            Assert.AreEqual(22, filesNames.Count);
        }
    }
}