using ErgastF1ApiServiceCache.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErgastF1ApiServiceCache.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonService _seasonService;

        public SeasonsController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeasons()
        {
            var seasons = await _seasonService.GetAsync();
            return Ok(seasons);
        }
    }
}
