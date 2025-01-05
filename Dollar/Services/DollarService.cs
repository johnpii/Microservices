using CoinGecko.Net.Clients;
using Dollar.Interfaces.Services;

namespace Dollar.Services
{
    public class DollarService : IDollarService
    {
        public async Task<decimal> GetDollarExchangeRate()
        {
            var restClient = new CoinGeckoRestClient();
            var tickerResult = await restClient.Api.GetMarketsAsync("RUB");
            var lastPrice = tickerResult.Data.Single(x => x.Name == "Tether").CurrentPrice;
            return lastPrice;
        }
    }
}

