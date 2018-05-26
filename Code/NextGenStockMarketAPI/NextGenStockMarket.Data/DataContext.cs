using NextGenStockMarket.Data.Entities;
using System.Collections.Generic;

namespace NextGenStockMarket.Data
{
    public class DataContext
    {
        public List<AllBankRecords> AllBankRecords { get; set; } = new List<AllBankRecords>();
        public List<AllStockMarketRecords> AllStockMarketRecords { get; set; } = new List<Entities.AllStockMarketRecords>();
    }
}
