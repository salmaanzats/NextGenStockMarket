using NextGenStockMarket.Data.Entities;

namespace NextGenStockMarket.Service.Interface
{
    public interface IClockService
    {
        void CreateClock(Clock clock);
        string PlayerTurn(Clock clock);
    }
}
