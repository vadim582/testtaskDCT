using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly HttpClient _httpClient;

        public CryptoService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoApp/1.0");
        }

        public async Task<List<Crypto>> GetTopCryptos()
        {
            var response = await _httpClient.GetStringAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=20&page=1");
            return JsonConvert.DeserializeObject<List<Crypto>>(response);
        }

        public async Task<Crypto> GetCryptoDetails(string id)
        {
            var response = await _httpClient.GetStringAsync($"https://api.coingecko.com/api/v3/coins/{id}");
            var cryptoDetails = JsonConvert.DeserializeObject<Crypto>(response);
            return cryptoDetails;
        }
    }
}