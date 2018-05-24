using NextGenStockMarket.Data.Entities;
using System.Threading.Tasks;

namespace NextGenStockMarket.Service.Interface
{
    public interface IBankService
    {
        Task<BankAccount> CreateBankAccount(BankAccount bank);
        Task<AllBankRecords> ShowBankBalance(string PlayerName);
        Task<AllBankRecords> Deposit(BankTransaction transaction);
        Task<AllBankRecords> Withdraw(BankTransaction transaction);
    }
}
