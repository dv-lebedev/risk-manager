
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
using RiskManager.Logic;

namespace RiskManager.Tests
{
    [TestClass]
    public class LinearRegressionTests
    {
        [TestMethod]
        public void Test()
        {
            double[] x = { 8, 11, 12, 9, 8, 8, 9, 9, 8, 12 };
            double[] y = { 5, 10, 10, 7, 5, 6, 6, 5, 6, 8 };

            var lr = new LinearRegression(x, y);
            lr.Calculate();

            Assert.AreEqual(-2.7540, lr.Alpha, 0.0001);
            Assert.AreEqual(1.0163, lr.Beta, 0.0001);
            Assert.AreEqual(0.8661, lr.R, 0.0001);
            Assert.AreEqual(0.7501, lr.RSquared, 0.0001);
            Assert.AreEqual(2.48, lr.Covariation, 0.0001);
        }
    }
}
