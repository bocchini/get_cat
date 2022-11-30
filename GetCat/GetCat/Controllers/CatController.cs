using GetCat.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GetCat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ICatService _catService;

        public CatController(ICatService catService)
        {
            _catService = catService;
        }

        [HttpPost()]
        public async Task<IActionResult> Get(int limit, int page)
        {
            var catResponse = await _catService.Get(limit, page);
            if (catResponse is null) return NotFound("Nenhum gato encontrado");
            return Ok(catResponse);
        }
    }
}
