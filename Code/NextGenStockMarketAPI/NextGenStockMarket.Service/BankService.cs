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
        protected readonly IClockService clockService;
        protected readonly IGameService gameService;

        public BankService(IClockService _clockService, IGameService _gameService)
        {
            cache = new MemoryCacheManager();
            clockService = _clockService;
            gameService = _gameService;
        }

        public async Task<BankAccount> CreateBankAccount(BankAccount bank)
        {
            var players = new Players();
            players.PlayerName = bank.PlayerName;

            var status = gameService.CreatePlayer(players);
            if (status == "Created")
            {

                var Bank = cache.Get<BankAccount>(bank.PlayerName + "_Bank");

                if (Bank != null)
                {
                    if (Bank.PlayerName == bank.PlayerName)
                    {
                        throw new Exception("Account name already exists");
                    }
                }

                var clock = new Clock();
                clock.PlayerName = bank.PlayerName;
                clock.PlayerTurn = 0;
                this.clockService.CreateClock(clock);

                BankAccount newPlayer = new BankAccount() { };
                newPlayer.PlayerName = bank.PlayerName;
                newPlayer.Balance = bank.Balance;

                var bankRecords = new AllBankRecords() { };
                bankRecords.Accounts = newPlayer;

                cache.Set(newPlayer.PlayerName + "_Bank", bankRecords, Constants.cacheTime);
                return newPlayer;
            }
            return null;
        }

        public async Task<AllBankRecords> GetBankAccount(BankAccount bank)
        {
            var playerBank = cache.Get<AllBankRecords>(bank.PlayerName + "_Bank");

            if (playerBank == null)
            {
                throw new Exception("No bank account exists for provided player");
            }
           
            return playerBank;
        }

        public async Task<AllBankRecords> ShowBankBalance(string playerName)
        {
            var playerAccount = cache.Get<AllBankRecords>(playerName + "_Bank");

            if (playerAccount == null)
            {
                throw new Exception("No bank account exists for provided player");
            }

            return playerAccount;
        }

        public async Task<AllBankRecords> Deposit(BankTransaction transaction)
        {
            var playerAccount = cache.Get<AllBankRecords>(transaction.PlayerName + "_Bank");
            var turn = cache.Get<Clock>(transaction.PlayerName + "_Clock");

            if (playerAccount == null)
            {
                throw new Exception("No bank account exists for provided player");
            }

            var clock = new Clock();
            clock.PlayerName = turn.PlayerName;
            clock.PlayerTurn += 1;
            var gameStatus = this.clockService.PlayerTurn(clock);
            if (gameStatus == Constants.gameOver)
            {
                throw new Exception("Game Over");
            }

            playerAccount.Accounts.Balance += transaction.Price;

            var bankRecords = new AllBankRecords() { };
            bankRecords = playerAccount;
            bankRecords.BankTransactions.Add(transaction);

            cache.Set(transaction.PlayerName + "_Bank", bankRecords, Constants.cacheTime);
            return bankRecords;
        }

        public async Task<AllBankRecords> Withdraw(BankTransaction transaction)
        {
            var playerAccount = cache.Get<AllBankRecords>(transaction.PlayerName + "_Bank");
            var turn = cache.Get<Clock>(transaction.PlayerName + "_Clock");

            if (playerAccount == null)
            {
                throw new Exception("No bank account exists for provided player");
            }

            var clock = new Clock();
            clock.PlayerName = turn.PlayerName;
            clock.PlayerTurn += 1;
            var gameStatus = this.clockService.PlayerTurn(clock);
            if (gameStatus == Constants.gameOver)
            {
                throw new Exception("Game Over");
            }

            playerAccount.Accounts.Balance -= transaction.Price;

            var bankRecords = new AllBankRecords() { };
            bankRecords = playerAccount;
            bankRecords.BankTransactions.Add(transaction);

            cache.Set(transaction.PlayerName + "_Bank", bankRecords, Constants.cacheTime);
            return bankRecords;
        }
    }
}
