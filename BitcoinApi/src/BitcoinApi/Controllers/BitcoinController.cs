using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using BitcoinApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BitcoinApi.Controllers
{
    [Route("api/[controller]")]
    public class BitcoinController : ControllerBase
    {
        private readonly ICoinbaseService _coinbaseService;

        public BitcoinController(ICoinbaseService coinbaseService)
        {
            _coinbaseService = coinbaseService;
        }

        [HttpGet("{baseCurrency}")]
        public async Task<IActionResult> Get(string baseCurrency)
        {
            try
            {
                var price = await _coinbaseService.GetBitcoinPrice("BTC", baseCurrency.ToUpper());

                return Ok(price);
            }
            catch(Exception ex)
            {
                LambdaLogger.Log(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
