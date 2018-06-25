using NextGenStockMarket.Service.Interface;
using System.Threading.Tasks;
using System.Web.Http;
using NextGenStockMarket.Data.Entities;

namespace NextGenStockMarketAPI.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class StockMarketController : ApiController
    {
        private IStockMarketService stockMarketService;

        public StockMarketController(IStockMarketService _stockMarketService)
        {
            stockMarketService = _stockMarketService;
        }

        [HttpGet, Route("market/stock")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await stockMarketService.GetMarketData());
        }

        [HttpGet, Route("market/getprice")]
        public int GetPrice(string sector, string stock, int turn)
        {
            return stockMarketService.GetPrice(sector, stock, turn);
        }

        [HttpGet, Route("market/stockanalyst")]
        public StockAnalyst Stockanalyst()
        {
            return stockMarketService.Stockanalyst();
        }

        [HttpGet, Route("market/sectoranalyst")]
        public SectorAnalyst Sectoranalyst()
        {
            return stockMarketService.Sectoranalyst();
        }
    }
}