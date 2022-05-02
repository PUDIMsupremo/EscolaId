using Microsoft.AspNetCore.Mvc;

namespace EscolaId.Controllers
{
    [ApiController]

    public class HomeControllers : ControllerBase
    {
        [HttpGet("/Health")]
        public IActionResult Get()
        {
            return Ok();
        }
        
    }

}