
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
using System.Globalization;
using System.IO;

namespace RiskManager.Data
{
    public class CSVMarketData : IMarketDataProvider
    {
        public string CsvDirectory { get; private set; }

        public CSVMarketData(string csvDirectory)
        {
            CsvDirectory = csvDirectory;
        }

        public List<Stock> Get(string symbol, DateTime first, DateTime last)
        {
            string[] lines = File.ReadAllLines(Path.Combine(CsvDirectory, symbol + ".txt"));

            var result = new List<Stock>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] cuts = lines[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (cuts.Length == 0)
                    throw new FormatException("Check csv files format.");

                decimal price = decimal.Parse(cuts[5], CultureInfo.InvariantCulture);
                DateTime dt = DateTime.ParseExact(cuts[0], "yyyyMMdd", CultureInfo.InvariantCulture);

                if (price <= 0) throw new FormatException($" price = {price} in {symbol}");

                if ((dt >= first) && (dt <= last))
                {
                    result.Add(new Stock { DateTime = dt, Price = price });
                }
            }
            return result;
        }

        public List<MainTableItem> GetMainTable()
        {
            var result = new List<MainTableItem>();

            foreach (var file in Directory.GetFiles(CsvDirectory))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                result.Add(new MainTableItem { Symbol = fileName });
            }
            return result;
        }
    }
}
