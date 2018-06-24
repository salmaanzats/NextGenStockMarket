using System.Collections.Generic;

namespace NextGenStockMarket.Data.Entities
{
    public class Clock
    {
        public string PlayerName { get; set; }
        public int PlayerTurn { get; set; }
        public int Turn { get; set; } = 0;
    }
}
