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

    public class Event
    {
        public double EventSum { get; set; }
        public string Type { get; set; }

    }

    public class EventRecord
    {
        public int NextTurn { get; set; }
        public int LastTurn { get; set; }
        public double EventSum { get; set; }
        public int Duration { get; set; }
    }

    public class MainEventRecord
    {
        public string Sector { get; set; }
        public string Stock { get; set; }
        public int Value { get; set; }
    }
    public class PrevVal
    {
        public int Value { get; set; }
    }
    public class AllData
    {
        public decimal Value { get; set; }
    }
    //public class AllDataRecord
    //{
    //    public string Sector { get; set; }
    //    public string Stock { get; set; }
    //    public List<AllData> record { get; set; } = new List<AllData>();
    //}
    public class AllData1
    {
        public List<AllData> Value { get; set; } = new List<AllData>();
    }
}
