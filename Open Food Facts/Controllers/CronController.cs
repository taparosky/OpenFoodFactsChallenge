using Microsoft.AspNetCore.Mvc;
using OpenFoodFacts.Models;
using OpenFoodFacts.Services;

namespace OpenFoodFacts.Controllers
{
    [Route("api")]
    [ApiController]
    public class CronController : ControllerBase
    {
        private readonly CronService _cronService;

        public CronController(CronService CronService) => _cronService = CronService;

        [HttpGet]
        public async Task<Cron> Get() => await _cronService.GetAsync();
    }
}
