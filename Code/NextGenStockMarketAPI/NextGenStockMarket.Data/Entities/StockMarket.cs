using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenStockMarket.Data.Entities
{
    public class StockMarket
    {
        public string CompanyName { get; set; } 
    }

    public class Sector {
        public string SectorName { get; set; }
        public decimal StockPrice { get; set; }
    }

    public class AllStockMarketRecords
    {
        public StockMarket StockMarket { get; set; } = new StockMarket();
        public List<Sector> Sectors { get; set; } = new List<Sector>();
    }
}
