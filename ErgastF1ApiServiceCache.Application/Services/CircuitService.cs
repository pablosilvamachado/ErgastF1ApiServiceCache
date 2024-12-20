using ErgastF1ApiServiceCache.Application.Interfaces;
using ErgastF1ApiServiceCache.Domain.Entities;
using ErgastF1ApiServiceCache.Domain.Interfaces.Repositories;
using ErgastF1ApiServiceCache.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgastF1ApiServiceCache.Application.Services
{
    public class CircuitsService : ICircuitService
    {
        private readonly ICacheService _cacheService;
        private readonly ICircuitRepository _repository;

        public CircuitsService(ICacheService cacheService, ICircuitRepository repository)
        {
            _cacheService = cacheService;
            _repository = repository;
        }

        public async Task<List<Circuit>> GetAsync()
        {
            string cacheKey = "circuits";

            var cached = await _cacheService.GetAsync<List<Circuit>>(cacheKey);
            if (cached != null)
            {
                return cached;
            }

            var seasons = await _repository.GetAsync();
            await _cacheService.SetAsync(cacheKey, seasons, TimeSpan.FromHours(1));

            return seasons;
        }
    }
}
