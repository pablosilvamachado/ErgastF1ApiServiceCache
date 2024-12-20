using ErgastF1ApiServiceCache.Domain.Entities;
using ErgastF1ApiServiceCache.Domain.Interfaces.Repositories;
using ErgastF1ApiServiceCache.Domain.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ErgastF1ApiServiceCache.Infrastructure.Repositories
{
    public class CircuitRepository:ICircuitRepository
    {

        private readonly HttpClient _httpClient;

        public CircuitRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Circuit>> GetAsync()
        {
            var response = await _httpClient.GetAsync("http://api.jolpi.ca/ergast/f1/circuits");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<Wrapper>(content);
            return wrapper.MRData.CircuitTable.Circuits;
        }
    }
}
