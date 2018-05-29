using System.Collections.Generic;

namespace NextGenStockMarket.Data.Entities
{
    public class BankAccount
    {
        public string PlayerName { get; set; }
        public decimal Balance { get; set; }
    }

    public class BankTransaction
    {
        public string PlayerName { get; set; }
        public int Turn { get; set; }
        public string Transceiver { get; set; }
        public decimal Price { get; set; }
    }

    public class AllBankRecords
    {
        public BankAccount Accounts { get; set; }
        public ICollection<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();
    }
}
