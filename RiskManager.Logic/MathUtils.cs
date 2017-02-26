﻿
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

using System;
using System.Linq;

namespace RiskManager.Logic
{
    public static class MathUtils
    {
        public static double PowArray(double[] array)
        {
            double total = .0;
            for (int i = 0; i < array.Length; i++)
            {
                total += Math.Pow(array[i], 2);
            }
            return total;
        }

        public static double MultiplyArrays(double[] x, double[] y)
        {
            double total = .0;
            for (int i = 0; i < x.Length; i++)
            {
                total += x[i] * y[i];
            }
            return total;
        }

        public static double StandardDeviation(double[] array)
        {
            double result = .0;
            double averageValue = array.Average();
            for (int i = 0; i < array.Length; i++)
            {
                result += Math.Pow(array[i] - averageValue, 2);
            }
            return Math.Sqrt(result /= (array.Length - 1));
        }
    }
}