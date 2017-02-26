
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

namespace RiskManager.Logic
{
    public class RiskParameters
    {
        private double weight;
        private double tradeLimit;
        private double risk;
        private double commission;
        private LinearRegression regression;

        public double Weight
        {
            get { return weight; }
            set
            {
                if (value < 0) throw new ArgumentException("weight < 0");

                weight = value;
            }
        }
        public double TradeLimit
        {
            get { return tradeLimit; }
            set
            {
                if (tradeLimit < 0) throw new ArgumentException("tradeLimit < 0");

                tradeLimit = value;
            }
        }
        public double Risk
        {
            get { return risk; }
            set
            {
                if (risk < 0) throw new ArgumentException("risk < 0");

                risk = value;
            }
        }
        public double Commission
        {
            get { return commission; }
            set
            {
                if (commission < 0) throw new ArgumentException("commission < 0");

                commission = value;
            }
        }
        public LinearRegression Regression
        {
            get { return regression; }
            set { regression = value; }
        }

        public RiskParameters(LinearRegression regression, double weight,
                          double tradeLimit, double risk, double commission)
        {
            if (regression == null) throw new ArgumentNullException(nameof(regression));

            Regression = regression;
            Weight = weight;
            TradeLimit = tradeLimit;
            Risk = risk;
            Commission = commission;
        }
    }
}
