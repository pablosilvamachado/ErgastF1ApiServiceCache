using ErgastF1ApiServiceCache.Domain.Entities;

namespace ErgastF1ApiServiceCache.Application.Interfaces
{
    public interface ISeasonService
    {
        Task<List<Season>> GetSeasonsAsync();
    }
}
