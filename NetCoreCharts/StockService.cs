using NetCoreCharts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreCharts
{
    public enum StockTime { OneMonth, SixMonth, OneYear, FiveYear }

    public class StockService
    {
        public StockService()
        {

        }

        public async Task<List<StockPoint>> GetStockHistory(StockViewModel model)
        {
            string timeParam = "6m";

            switch (model.time)
            {
                case StockTime.FiveYear:
                    timeParam = "5y";
                    break;
                case StockTime.OneYear:
                    timeParam = "1y";
                    break;
                case StockTime.SixMonth:
                    timeParam = "6m";
                    break;
                case StockTime.OneMonth:
                    timeParam = "1m";
                    break;
                default:
                    timeParam = "6m";
                    break;

            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    string url = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/{1}", model.ticker, timeParam);

                    var response = await httpClient.GetAsync(url);

                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        if (jsonString == "Unknown symbol")
                        {
                            model.error = jsonString;
                            return null;
                        }

                        return JsonConvert.DeserializeObject<List<StockPoint>>(jsonString);
                    }

                    model.error = "No response";
                    return null;
                }
            }
            catch (Exception ex)
            {
                model.error = ex.Message;
                return null;
            }
        }
    }
}
