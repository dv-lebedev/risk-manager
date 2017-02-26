
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
using System.Collections.Generic;

namespace RiskManager.Logic
{
    public class RiskCalculation
    {
        private string indexSymbol;
        private double balance;
        private double risk;
        private double commission;
        private bool initFlag;
        private Dictionary<string, double[]> historicalPrices;
        private Dictionary<string, RiskParameters> riskParameters;

        public Dictionary<string, RiskParameters> RiskParameters
        {
            get
            {
                return riskParameters;
            }
        }

        public RiskCalculation(Dictionary<string, double[]> historicalPrices, string indexSymbol,
                               double balance, double risk, double commission)
        {
            if (historicalPrices == null) throw new ArgumentNullException(nameof(historicalPrices));
            if (indexSymbol == null) throw new ArgumentNullException(nameof(indexSymbol));

            this.indexSymbol = indexSymbol;
            this.balance = balance;
            this.risk = risk;
            this.commission = commission;
            this.historicalPrices = historicalPrices;
            this.riskParameters = new Dictionary<string, RiskParameters>();
        }

        public void Initialize(double rValueMinimum = 0)
        {
            if (initFlag) throw new Exception("It has been initialized.");

            SetRegressions(rValueMinimum);
            SetParams();
            initFlag = true;
        }

        private void CreateRiskParameter(string symbol, LinearRegression regression)
        {
            riskParameters.Add(symbol, new RiskParameters(regression, 0, 0, 0, 0));
        }

        private void SetRegressions(double rValueMinimum)
        {
            double[] indexValues = historicalPrices[indexSymbol];

            foreach (var item in historicalPrices) {
                if (item.Key != indexSymbol) {
                    double[] prices = historicalPrices[item.Key];
                    var lr = new LinearRegression(prices, indexValues);
                    lr.Calculate();
                    if (Math.Abs(lr.R) >= Math.Abs(rValueMinimum)) {
                        CreateRiskParameter(item.Key, lr);
                    }
                }
            }
        }

        private void SetParams()
        {
            double totalWeight = .0;
            foreach (var item in RiskParameters)
            {
                string symbol = item.Key;
                double b1 = riskParameters[symbol].Regression.Beta;
                double weight = 1.0 / (1.0 + Math.Abs(b1));
                riskParameters[symbol].Weight = weight;
                totalWeight += weight;
            }
            foreach (var item in RiskParameters)
            {
                string symbol = item.Key;
                double oldWeight = riskParameters[symbol].Weight;
                double weight = oldWeight / totalWeight;
                RiskParameters rp = item.Value;
                rp.Weight = weight;
                rp.TradeLimit = balance * weight;
                rp.Risk = balance * weight * risk / 100.0;
                rp.Commission = balance * weight * commission / 100.0;
            }
        }
    }
}
