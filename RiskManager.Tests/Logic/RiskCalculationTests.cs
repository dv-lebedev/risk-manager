
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

namespace RiskManager.Logic.Tests
{
    [TestClass()]
    public class RiskCalculationTests
    {
        private const string HISTORICAL_PRICES_PATH = "historical-prices";

        [TestMethod()]
        public void InitializeTest()
        {
            var historicalPrices = CsvUtils.ReadAllDataFrom(HISTORICAL_PRICES_PATH, 4, false);
            string indexSymbol = "SP500";

            Assert.AreEqual(true, historicalPrices.ContainsKey(indexSymbol));
    
            RiskCalculation rc = new RiskCalculation(historicalPrices, indexSymbol, 100000.00, 0.5, 0.15);
            rc.Initialize(0);

            Assert.AreEqual(21, rc.RiskParameters.Count);

            RiskParameters jpm = rc.RiskParameters["JPM"];

            Assert.AreEqual(70.8198, jpm.Regression.Beta, 0.0001);
            Assert.AreEqual(0.8506, jpm.Regression.R, 0.0001);
            Assert.AreEqual(0.0094, jpm.Weight, 0.0001);
        }
    }
}