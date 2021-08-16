using Extension;
using Microsoft.Extensions.Options;
using Service.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryResponse>> GetCountriesAsync();
    }

    public class CountryService : ICountryService
    {
        private readonly HttpClient _client;
        private readonly CountryOptions _options;

        public CountryService(HttpClient client, IOptions<CountryOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<IEnumerable<CountryResponse>> GetCountriesAsync()
        {
            var response = await _client.GetAsync($"{ _options.BaseUrl }/rest/v2/all");

            if (response.IsSuccessStatusCode)
            {
                return await response.Deserialize<IEnumerable<CountryResponse>>();
            }

            return default;
        }
    }
}
