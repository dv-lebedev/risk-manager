
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

using RiskManager.Data;
using RiskManager.Logic;
using RiskManager.WpfApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RiskManager.WpfApp
{
    public partial class MainWindow : Window
    {
        private IMarketDataProvider GetDataProvider() => new CSVMarketData("csv-market-data-2016");

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try
            {
                var repository = GetDataProvider();
                var maintable = repository.GetMainTable();

                foreach(var item in maintable)
                {
                    index.Items.Add(item.Symbol);
                }
                index.Text = index.Items[0].ToString();
            }
            catch(Exception ex)
            {
                this.Display(ex.Message);
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var repository = GetDataProvider();
                var maintable = repository.GetMainTable();

                var values = new Dictionary<string, double[]>();

                foreach (var item in maintable)
                {
                    var stockValues = repository.Get(item.Symbol, dtFirst.GetSelectedValue(), dtLast.GetSelectedValue());
                    values.Add(item.Symbol, stockValues.Select(i => (double)i.Price).ToArray());
                }

                var rc = new RiskCalculation(values, index.Text, tradeVolume.GetDouble(), risk.GetDouble(), commission.GetDouble());
                rc.Initialize();

                FillDataGrid(rc.RiskParameters);
            }
            catch(Exception ex)
            {
                this.Display(ex.Message);
            }
        }

        private void FillDataGrid(Dictionary<string, RiskParameters> riskParameters)
        {
            var result = new List<RCResultItem>();

            foreach(var item in riskParameters)
            {
                string symbol = item.Key;
                var value = item.Value;
                var regression = item.Value.Regression;

                result.Add(new RCResultItem
                {
                    Symbol = symbol,
                    Alpha = regression.Alpha,
                    Beta = regression.Beta,
                    R = regression.R,
                    RSquared = regression.RSquared,
                    TradeLimit = value.TradeLimit,
                    RiskLimit = value.Risk,
                    Commission = value.Commission,
                    Weight = value.Weight
                });
            }
            dataGrid.ItemsSource = result;
        }
    }
}
