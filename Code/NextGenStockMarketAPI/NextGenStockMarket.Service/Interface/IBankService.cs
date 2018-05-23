using NextGenStockMarket.Data.Entities;
using System.Threading.Tasks;

namespace NextGenStockMarket.Service.Interface
{
    public interface IBankService
    {
        Task<Bank> CreateBankAccount(Bank bank);
    }
}
