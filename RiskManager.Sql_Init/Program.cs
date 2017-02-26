
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

using RiskManager.Logic;
using System;
using System.Data.Linq;
using System.Globalization;

namespace RiskManager.Sql_Init
{
    class Program
    {
        private static string ConnectionString = @"Server=LEBEDEV\SQLEXPRESS01;Database=marketdatatest;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            using (var ctx = new DataContext(ConnectionString))
            {
                string path = "csv-market-data-2016";

                var historicalData = CsvUtils.ReadAllDataFrom(path, 5);

                foreach (var item in historicalData)
                {
                    ctx.ExecuteCommand($@"CREATE TABLE {item.Key} (
                                    Id INT IDENTITY(1,1) NOT NULL,
	                                DateTime DATETIME NOT NULL,
	                                Price NUMERIC(18, 6) NOT NULL)");

                    DateTime dt = new DateTime(2016, 1, 1);

                    foreach (var value in item.Value)
                    {
                        string dtString = dt.ToString("yyyy-MM-dd");
                        string valueString = value.ToString(CultureInfo.InvariantCulture);

                        ctx.ExecuteCommand($"INSERT INTO {item.Key} (DateTime, Price) VALUES ('{dt}', {valueString})");
                        dt = dt.AddDays(1);
                    }
                }
            }
        }
    }
}
