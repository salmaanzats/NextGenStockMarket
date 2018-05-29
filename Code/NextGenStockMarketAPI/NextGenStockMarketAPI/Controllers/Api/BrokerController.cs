using NextGenStockMarket.Service.Interface;
using System.Threading.Tasks;
using System.Web.Http;
using static NextGenStockMarket.Data.Entities.Broker;

namespace NextGenStockMarketAPI.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class BrokerController : ApiController
    {
        private IBrokerService brokerService;

        public BrokerController(IBrokerService _brokerService)
        {
            brokerService = _brokerService;
        }

        [HttpPost, Route("createaccount")]
        public async Task<IHttpActionResult> Create(string playerName)
        {
            return Ok(await brokerService.CreateAccount(playerName));
        }


        [HttpGet, Route("getcompanies")]
        public async Task<IHttpActionResult> GetCompanies()
        {
            return Ok(await brokerService.GetCompany());
        }

        [HttpGet, Route("getsectors")]
        public async Task<IHttpActionResult> GetSectors(string companyName)
        {
            return Ok(await brokerService.GetSector(companyName));
        }

        [HttpPost, Route("buy")]
        public async Task<IHttpActionResult> Buy(BrokerInfo brokerInfo)
        {
            return Ok(await brokerService.BuyStock(brokerInfo));
        }

        [HttpPost, Route("sell")]
        public async Task<IHttpActionResult> Sell(BrokerInfo brokerInfo)
        {
            return Ok(await brokerService.SellStock(brokerInfo));
        }

        [HttpGet, Route("portfolio")]
        public async Task<IHttpActionResult> GetPortfolio(string playerName)
        {
            return Ok(await brokerService.Portfolio(playerName));
        }
    }
}