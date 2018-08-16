using NetCoreCharts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            StockService s = new StockService();
            Task<List<StockPoint>> result = s.GetStockHistory("AAPL", StockTime.SixMonth);
            result.Wait();
            List<StockPoint> points = result.Result;
        }
    }
}
