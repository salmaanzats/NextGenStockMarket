using System.Collections.Generic;

namespace NextGenStockMarket.Data.Entities
{
    public class Broker
    {
        public class BrokerAccount
        {
            public string PlayerName { get; set; }
        }

        public class BrokerInfo
        {
            public string Sector { get; set; }
            public int Quantity { get; set; }
            public decimal StockPrice { get; set; }
        }

        public class AllBrokerData
        {
            public BrokerAccount Accounts { get; set; }
            public ICollection<BrokerInfo> BrokerInfos { get; set; } = new List<BrokerInfo>();
        }
    }
}
