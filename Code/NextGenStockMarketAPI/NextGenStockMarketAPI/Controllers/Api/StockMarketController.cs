using NextGenStockMarket.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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

        [HttpGet, Route("stock")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await stockMarketService.GetMarketData());
        }

        [HttpGet, Route("getcompanies")]
        public async Task<IHttpActionResult> GetCompanies()
        {
            return Ok(await stockMarketService.getCompany());
        }

        [HttpGet, Route("getsectors")]
        public async Task<IHttpActionResult> GetSectors(string companyName)
        {
            return Ok(await stockMarketService.getSector(companyName));
        }

    }
}