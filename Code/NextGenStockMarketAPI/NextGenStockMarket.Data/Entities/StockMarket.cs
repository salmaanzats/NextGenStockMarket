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
        public List<Sector> Sectors { get; set; }
    }

    public class Sector {
        public string SectorName { get; set; }
        public string StockPrice { get; set; }
    }
}
