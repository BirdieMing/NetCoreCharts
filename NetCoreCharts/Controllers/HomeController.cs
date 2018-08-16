using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreCharts.Models;

namespace NetCoreCharts.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index([Bind("ticker", "time", "points")] StockViewModel sm)
        {
            if (string.IsNullOrEmpty(sm.ticker))
                return View(sm);

            if (sm.points == null)
                sm.points = new List<StockPoint>();
            try
            {
                StockService s = new StockService();
                Task<List<StockPoint>> res = s.GetStockHistory(sm);
                res.Wait();

                if (res.Result == null || res.Result.Count == 0)
                    return View(sm);

                sm.points = res.Result;

                sm.timeUnit = GetTimeUnit(sm.time);

                foreach (StockPoint p in sm.points)
                {
                    p.avg = (p.high + p.low) / 2;
                }

                return View(sm);
            }
            catch (Exception ex)
            {
                sm.error = ex.Message;
                return View(sm);
            }
        }

        public string GetTimeUnit(StockTime time)
        {
            switch (time)
            {
                case StockTime.FiveYear:
                    return "quarter";
                case StockTime.OneMonth:
                    return "day";
                case StockTime.SixMonth:
                    return "month";
                case StockTime.OneYear:
                    return "month";
                default:
                    return "";
            }
        }

        public IActionResult Index()
        {
            StockViewModel m = new StockViewModel();
            m.points = new List<StockPoint>();
            return View(m);  
        }  

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
