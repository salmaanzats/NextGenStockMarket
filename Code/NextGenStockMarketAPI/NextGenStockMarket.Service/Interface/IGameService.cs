using NextGenStockMarket.Data.Entities;

namespace NextGenStockMarket.Service.Interface
{
    public interface IGameService
    {
        string CreatePlayer(Players player);
        int GetConnectedPlayers();
        string GameStatus();
        AllBankRecords GetWinner();
        int NewGame();
    }
}
