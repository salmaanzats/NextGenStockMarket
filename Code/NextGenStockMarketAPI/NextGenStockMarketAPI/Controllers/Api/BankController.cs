using NextGenStockMarket.Data.Entities;
using NextGenStockMarket.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace NextGenStockMarketAPI.Controllers.Api
{
    [RoutePrefix("api/v1")]
    public class BankController : ApiController
    {
        private IBankService bankService;

        public BankController() { }

        public BankController(IBankService _bankService)
        {
            bankService = _bankService;
        }

        [HttpPost, Route("createAccount")]
        public async Task<IHttpActionResult> CreateAccount([FromBody]Bank bank)
        {
            return Ok(await bankService.CreateBankAccount(bank));

        }

        [HttpPost, Route("bank/deposit")]
        public async Task<IHttpActionResult> Deposit([FromBody]Bank bank)
        {
            throw new NotImplementedException();
        }

        [HttpPost, Route("bank/withdraw")]
        public async Task<IHttpActionResult> Withdraw([FromBody]Bank bank)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route("bank")]
        public async Task<IHttpActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route("banks/{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route("banks")]
        public async Task<IHttpActionResult> Update([FromBody]Bank bank)
        {
            return Ok();
        }

        [HttpDelete, Route("banks/{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}