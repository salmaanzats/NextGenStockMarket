using Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarket.Service
{
    public class GameService : IGameService
    {
        protected readonly ICacheManager cache;

        public GameService(IClockService _clockService)
        {
            cache = new MemoryCacheManager();
        }

        public string CreatePlayer(Players player)
        {
            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");

            if (connectedPlayers != null)
            {
                foreach (var p in connectedPlayers.Players)
                {
                    if (p.PlayerName == player.PlayerName)
                    {
                        throw new Exception("Player Name already exists.Please try a different player name");
                    }
                }

                if (connectedPlayers.Players.Count == Constants.maximumPlayers)
                {
                    throw new Exception("Can't create a account. Maximum players are connected!");
                }
                else
                {
                    connectedPlayers.Players.Add(player);
                    cache.Set("ConnectedPlayers", connectedPlayers, Constants.cacheTime);
                    return "Created";
                }
            }
            else
            {
                connectedPlayers = new AllPlayers();
                connectedPlayers.Players.Add(player);
                cache.Set("ConnectedPlayers", connectedPlayers, Constants.cacheTime);
                return "Created";
            }
        }

        public async Task<int> GetConnectedPlayers()
        {
            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");
            if (connectedPlayers != null)
            {
                var count = connectedPlayers.Players.Count;
                return count;
            }
            return 0;
        }

        public async Task<string> GameStatus()
        {
            int game = 0;
            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");
            if (connectedPlayers != null)
            {
                foreach (var players in connectedPlayers.Players)
                {
                    var playerBank = cache.Get<AllBankRecords>(players.PlayerName + "_Bank");

                    if (playerBank.CurrentTurn == Constants.gameTurns)
                    {
                        game += 1;
                    }
                }
            }
            if (game == Constants.maximumPlayers)
            {
                return Constants.gameOver;
            }
            return Constants.play;
        }

        public async Task<AllBankRecords> GetWinner()
        {
            decimal winnerScore = 0;
            var playerBank = new AllBankRecords();
            var winner = new AllBankRecords();

            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");
            if (connectedPlayers != null)
            {
                foreach (var players in connectedPlayers.Players)
                {
                    Dictionary<string, decimal> stockPrice = new Dictionary<string, decimal>();
                    stockPrice.Add(String.Format(players.PlayerName, 1.ToString()), 1);

                    Dictionary<string, decimal> score = new Dictionary<string, decimal>();
                    score.Add(String.Format(players.PlayerName, 1.ToString()), 1);

                    var stock = GetStockValue(players.PlayerName);
                    stockPrice[players.PlayerName] = stock;

                    playerBank = cache.Get<AllBankRecords>(players.PlayerName + "_Bank");

                    if (winnerScore < playerBank.Accounts.Balance + stockPrice[players.PlayerName])
                    {
                        winnerScore = playerBank.Accounts.Balance + stockPrice[players.PlayerName];
                        winner = playerBank;
                    }
                }
            }
            winner.Accounts.Balance = winnerScore;
            return winner;
        }

        public decimal GetStockValue(string playerName)
        {
            var playerPortfolio = cache.Get<AllBrokerData>(playerName + "_Broker");
            Dictionary<string, decimal> stockPrice = new Dictionary<string, decimal>();
            stockPrice.Add(String.Format(playerName, 1.ToString()), 1);

            foreach (var portfolio in playerPortfolio.BrokerInfos)
            {
                var market = cache.Get<List<AllStockMarketRecords>>(Constants.marketData);
                foreach (var sec in market)
                {
                    if (portfolio.Sector == sec.StockMarket.CompanyName)
                    {
                        foreach (var s in sec.Sectors)
                        {
                            if (portfolio.Stock == s.SectorName && portfolio.IsAvailable == true)
                            {
                                stockPrice[playerName] += s.StockPrice * portfolio.Quantity;
                            }
                        }
                    }
                }
            }
            return stockPrice[playerName];
        }

        public async Task<int> NewGame()
        {
            cache.Clear();
            return 0;
        }
    }
}
