using ErgastF1ApiServiceCache.Domain.Entities;
using ErgastF1ApiServiceCache.Domain.Interfaces.Repositories;
using ErgastF1ApiServiceCache.Domain.Models.Wrappers;
using System.Text.Json;


namespace ErgastF1ApiServiceCache.Infrastructure.Repositories
{
    public  class SeasonRepository : ISeasonRepository
    {

        private readonly HttpClient _httpClient;

        public SeasonRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<Season>> GetSeasonsAsync()
        {
            var response = await _httpClient.GetAsync("http://api.jolpi.ca/ergast/f1/seasons");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<Wrapper>(content);
            return wrapper.MRData.SeasonTable.Seasons;
        }
    }
}
