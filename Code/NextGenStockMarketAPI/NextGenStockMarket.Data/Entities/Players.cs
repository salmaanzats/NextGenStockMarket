using System.Collections.Generic;

namespace NextGenStockMarket.Data.Entities
{

    public class Players
    {
        public string PlayerName { get; set; }
    }

    public class AllPlayers
    {
        public List<Players> Players { get; set; } = new List<Players>();
    }
}
