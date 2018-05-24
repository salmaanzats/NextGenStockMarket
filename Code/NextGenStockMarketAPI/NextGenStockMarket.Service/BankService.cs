using Inx.CarWash.Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System.Threading.Tasks;
using System;

namespace NextGenStockMarket.Service
{
    public class BankService : IBankService
    {
        protected readonly ICacheManager cache;

        public BankService()
        {
            cache = new MemoryCacheManager();
        }

        public async Task<BankAccount> CreateBankAccount(BankAccount bank)
        {
            var Bank = cache.Get<BankAccount>(bank.PlayerName);

            if (Bank != null)
            {
                if (Bank.PlayerName == bank.PlayerName)
                {
                    throw new Exception("Account name already exists");
                }
            }

            BankAccount newPlayer = new BankAccount() { };
            newPlayer.PlayerName = bank.PlayerName;
            newPlayer.Balance = bank.Balance;

            var bankRecords = new AllBankRecords() { };
            bankRecords.Accounts = newPlayer;
           
            cache.Set(newPlayer.PlayerName, bankRecords, Constants.cacheTime);
            return newPlayer;
        }

        public async Task<AllBankRecords> ShowBankBalance(string playerName)
        {
            var playerAccount = cache.Get<AllBankRecords>(playerName);

            if (playerAccount == null)
            {
                throw new Exception("No bank account exist for provided data");
            }

            return playerAccount;
        }

        public async Task<AllBankRecords> Deposit(BankTransaction transaction)
        {
            var playerAccount = cache.Get<AllBankRecords>(transaction.PlayerName);

            if (playerAccount == null)
            {
                throw new Exception("No bank account exist for provided data");
            }

            playerAccount.Accounts.Balance += transaction.Price;

            var bankRecords = new AllBankRecords() { };
            bankRecords = playerAccount;
            bankRecords.BankTransactions.Add(transaction);

            cache.Set(transaction.PlayerName, bankRecords, Constants.cacheTime);
            return bankRecords;
        }

        public async Task<AllBankRecords> Withdraw(BankTransaction transaction)
        {
            var playerAccount = cache.Get<AllBankRecords>(transaction.PlayerName);

            if (playerAccount == null)
            {
                throw new Exception("No bank account exist for provided data");
            }

            playerAccount.Accounts.Balance -= transaction.Price;

            var bankRecords = new AllBankRecords() { };
            bankRecords = playerAccount;
            bankRecords.BankTransactions.Add(transaction);

            cache.Set(transaction.PlayerName, bankRecords, Constants.cacheTime);
            return bankRecords;
        }
    }
}
