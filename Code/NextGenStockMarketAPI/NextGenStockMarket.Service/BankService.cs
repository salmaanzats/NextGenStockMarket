using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using System.Threading.Tasks;

namespace NextGenStockMarket.Service
{
    public class BankService: IBankService
    {
        public async Task<Bank> CreateBankAccount(Bank bank)
        {
            var newPlayer = new Bank()
            {
                Turn = bank.Turn,
                PlayerName = bank.PlayerName
            };

            return newPlayer;
        }
    }
}
