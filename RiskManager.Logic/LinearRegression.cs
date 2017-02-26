
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
    public class LinearRegression
    {
        public double Alpha { get; private set; }
        public double Beta { get; private set; }
        public double R { get; private set; }
        public double RSquared { get; private set; }
        public double Covariation { get; private set; }
        public double[] X { get; private set; }
        public double[] Y { get; private set; }

        public LinearRegression(double[] x, double[] y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            X = x;
            Y = y;
        }

        public void Calculate()
        {
            int N = X.Length;
            double xAverage = X.Average();
            double yAverage = Y.Average();
            double sx2 = MathUtils.PowArray(X) / N - Math.Pow(xAverage, 2);
            double xy = MathUtils.MultiplyArrays(X, Y);
            Covariation = xy / N - xAverage * yAverage;
            Beta = Covariation / sx2;
            Alpha = yAverage - Beta * xAverage;
            R = Beta * (MathUtils.StandardDeviation(X) / MathUtils.StandardDeviation(Y));
            RSquared = Math.Pow(R, 2);
        }
    }
}
