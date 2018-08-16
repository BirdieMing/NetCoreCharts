using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCharts.Models
{
    public class StockViewModel
    {
        [DisplayName("Ticker")]
        public String ticker { get; set; }

        public List<StockPoint> points { get; set; }

        [DisplayName("Time Period")]
        public StockTime time { get; set; }

        public string timeUnit { get; set; }

        public string error { get; set; }
    }
}
