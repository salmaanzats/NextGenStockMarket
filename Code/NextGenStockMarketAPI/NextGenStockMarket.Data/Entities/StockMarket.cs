﻿using System;
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

    public class StockPriceUpdate
    {
        public int Turn { get; set; }
        public ScoreArray ScoreArray { get; set; } = new ScoreArray();
    }

    public class ScoreArray
    {
        public int RandomMarket  { get; set; }
        public int SectorTrend { get; set; }
        public int GeneralTrend  { get; set; }
        public int EventComponent { get; set; }
    }

    public class SectorTrend
    {
        public int SectorTrendValue { get; set; }
    }
}
