using System.Collections.Generic;

namespace NextGenStockMarket.Data.Entities
{
    public class Bank
    {
        public string PlayerName { get; set; }
        public int Turn { get; set; }
        public List<BankTransactions> BankTransactions { get; set; }
    }

    public class BankTransactions
    {
        public string Transceiver { get; set; }
        public decimal Price { get; set; }
    }
}
