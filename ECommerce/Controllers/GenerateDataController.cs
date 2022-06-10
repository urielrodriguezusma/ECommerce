using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateDataController : ControllerBase
    {
        private readonly StoreContext context;

        public GenerateDataController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await StoreContextSeed.SeedAsync(this.context);
            return Ok();
        }

    }
}
