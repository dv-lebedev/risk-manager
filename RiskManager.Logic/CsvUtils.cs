
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

namespace RiskManager.Logic
{
    public static class CsvUtils
    {
        public static Dictionary<string, double[]> ReadAllDataFrom(string directory, int priceIndex, bool containsHeader = false)
        {
            Check.NotNull(directory);

            if (priceIndex < 0) throw new ArgumentException("[priceIndex] can't be less than 0.");

            var stocks = new Dictionary<string, double[]>();

            foreach (string file in Directory.EnumerateFiles(directory))
            {
                if (file.EndsWith(".txt") || file.EndsWith(".csv"))
                {
                    double[] values = Read(file, priceIndex, containsHeader);

                    string name = Path.GetFileNameWithoutExtension(file);

                    stocks.Add(name, values);
                }
            }

            if (stocks.Count == 0) throw new Exception("Files are not found.");
            
            return stocks;
        }

        public static double[] Read(string path, int priceIndex, bool containsHeader = false)
        {
            string[] lines = File.ReadAllLines(path);

            var result = new List<double>();

            int startlineCount = containsHeader ? 1 : 0;

            for (int i = startlineCount; i < lines.Length; i++)
            {
                string[] cuts = lines[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (cuts.Length == 0)
                    throw new FormatException("Check csv files format.");

                double price = double.Parse(cuts[priceIndex], CultureInfo.InvariantCulture);

                if (price <= 0) throw new FormatException($" price = {price} in {path}");
                
                result.Add(price);
            }
            return result.ToArray();
        }

        public static List<string> GetFilesNames(string path)
        {
            var result = new List<string>();

            foreach (var file in Directory.GetFiles(path))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                result.Add(fileName);
            }
            return result;
        }
    }
}
