using ErgastF1ApiServiceCache.Application.Interfaces;
using ErgastF1ApiServiceCache.Domain.Entities;
using ErgastF1ApiServiceCache.Domain.Interfaces;
using ErgastF1ApiServiceCache.Domain.Models;
using System.Text.Json;
using System.Configuration;
using ErgastF1ApiServiceCache.Domain.Interfaces.Repositories;


namespace ErgastF1ApiServiceCache.Application.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly ICacheService _cacheService;
        private readonly ISeasonRepository _seasonRepository;

        public SeasonService(ICacheService cacheService, ISeasonRepository seasonRepository)
        {
            _cacheService = cacheService;
            _seasonRepository = seasonRepository;
        }

        public async Task<List<Season>> GetSeasonsAsync()
        {
            string cacheKey = "seasons";

            var cachedSeasons = await _cacheService.GetAsync<List<Season>>(cacheKey);
            if (cachedSeasons != null)
            {
                return cachedSeasons;
            }

            var seasons = await _seasonRepository.GetSeasonsAsync();
            await _cacheService.SetAsync(cacheKey, seasons, TimeSpan.FromHours(1));

            return seasons;
        }      
    }
}
