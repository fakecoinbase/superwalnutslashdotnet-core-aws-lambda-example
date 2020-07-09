using System;
using System.Threading.Tasks;
using Coinbase;

namespace BitcoinApi.Services
{
    public interface ICoinbaseService
    {
        Task<decimal> GetBitcoinPrice(string code, string baseCurrency);
    }

    public class CoinbaseService : ICoinbaseService
    {
        private readonly CoinbaseClient _coinbaseClient;
        public CoinbaseService()
        {
            _coinbaseClient = new CoinbaseClient();
        }

        public async Task<decimal> GetBitcoinPrice(string code, string baseCurrency)
        {
            var r = await _coinbaseClient.Data.GetExchangeRatesAsync(code);

            if (!r.Data.Currency.Equals(code, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("Not found currency");
            }

            if(!r.Data.Rates.ContainsKey(baseCurrency))
            {
                throw new Exception("Not found base currency");
            }

            return r.Data.Rates[baseCurrency];
        }
    }
}
