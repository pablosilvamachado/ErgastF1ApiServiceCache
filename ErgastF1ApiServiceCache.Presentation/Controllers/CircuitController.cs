using ErgastF1ApiServiceCache.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErgastF1ApiServiceCache.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CircuitsController : ControllerBase
    {
        private readonly ICircuitService _service;

        public CircuitsController(ICircuitService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCircuits()
        {
            var seasons = await _service.GetAsync();
            return Ok(seasons);
        }
    }
}
