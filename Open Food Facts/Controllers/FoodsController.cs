using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenFoodFacts.Models;
using OpenFoodFacts.Models.Helpers;
using OpenFoodFacts.Models.Products;
using OpenFoodFacts.Models.Wrappers;
using OpenFoodFacts.Services;
using OpenFoodFacts.Services.Interfaces;
using OpenFoodFacts.Wrappers;

namespace OpenFoodFacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly FoodsService _foodsService;

        public FoodsController(FoodsService FoodsService) =>
        _foodsService = FoodsService;

       
           

        [HttpGet]
        public async Task<PagedResponse<List<Food>>> Get([FromQuery] PaginationFilter filter)
        {
            string route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await _foodsService.GetAsync(validFilter, route);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<Food>> Get(string code)
        {
            var Food = await _foodsService.GetAsync(code);

            if (Food is null)
            {
                return NotFound();
            }

            return Food;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Food newFood)
        {
            await _foodsService.CreateAsync(newFood);

            return CreatedAtAction(nameof(Get), new { code = newFood.Code }, newFood);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Food updatedFood)
        {
            var Food = await _foodsService.GetAsync(code);

            if (Food is null)
            {
                return NotFound();
            }

            updatedFood.Code = Food.Code;

            await _foodsService.UpdateAsync(code, updatedFood);

            return NoContent();
        }

        [HttpPut("delete/{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            var Food = await _foodsService.GetAsync(code);

            if (Food is null)
            {
                return NotFound();
            }

            await _foodsService.Delete(code);

            return NoContent();
        }
    }
}
