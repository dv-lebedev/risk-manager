
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
    public class MathUtilsTests
    {
        [TestMethod()]
        public void PowArrayTest()
        {
            double[] arr = { -2, 0, 7, 45, 127, 1024 };

            double result = MathUtils.PowArray(arr);

            Assert.AreEqual(1066783.0, result, 0);
        }

        [TestMethod()]
        public void MultiplyArraysTest()
        {
            double[] x = { 1, 3, 6, 12 };
            double[] y = { 2, 4, 7, 18 };

            double result = MathUtils.MultiplyArrays(x, y);

            Assert.AreEqual(272, result, 0);
        }

        [TestMethod()]
        public void StandardDeviationTest()
        {
            double[] arr = { 1, 3, 5, 8, 13, 24, 77, 128 };

            double stdev = MathUtils.StandardDeviation(arr);

            Assert.AreEqual(45.9376, stdev, 0.0001);
        }
    }
}