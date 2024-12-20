﻿using ErgastF1ApiServiceCache.Application.Interfaces;
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
        private readonly ISeasonRepository _repository;

        public SeasonService(ICacheService cacheService, ISeasonRepository repository)
        {
            _cacheService = cacheService;
            _repository = repository;
        }

        public async Task<List<Season>> GetAsync()
        {
            string cacheKey = "seasons";

            var cached = await _cacheService.GetAsync<List<Season>>(cacheKey);
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
