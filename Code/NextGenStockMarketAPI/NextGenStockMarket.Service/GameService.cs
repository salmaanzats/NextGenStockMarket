using Core.Cache;
using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using NextGenStockMarket.Service.Utility;
using System;

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

        public int GetConnectedPlayers()
        {
            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");
            if (connectedPlayers != null)
            {
                var count = connectedPlayers.Players.Count;
                return count;
            }
            return 0;
        }

        public string GameStatus()
        {
            int game = 0;
            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");
            if (connectedPlayers != null)
            {
                foreach (var players in connectedPlayers.Players)
                {
                    var playerBank = cache.Get<AllBankRecords>(players.PlayerName + "_Bank");

                    if (playerBank.CurrentTurn == Constants.maximumPlayers)
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

        public AllBankRecords GetWinner()
        {
            decimal Score = 0;
            AllBankRecords Winner = new AllBankRecords();
            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");
            if (connectedPlayers != null)
            {
                foreach (var players in connectedPlayers.Players)
                {
                    var playerBank = cache.Get<AllBankRecords>(players.PlayerName + "_Bank");

                    if (Score < playerBank.Accounts.Balance)
                    {
                        Score = playerBank.Accounts.Balance;
                        Winner = playerBank;
                    }

                }
            }
            return Winner;
        }

        public int NewGame()
        {
            var connectedPlayers = cache.Get<AllPlayers>("ConnectedPlayers");
            if (connectedPlayers != null)
            {
                foreach (var players in connectedPlayers.Players)
                {
                    cache.RemoveByStartwith(players.PlayerName);
                }
                return 0;
           }
            return 0;
        }
    }
}
