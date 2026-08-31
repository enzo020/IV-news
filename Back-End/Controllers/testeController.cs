using Microsoft.AspNetCore.Mvc;

namespace IVNews.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        // Endpoint: GET /api/test/ping
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new 
            { 
                Status = "Success",
                Message = "O back-end do IV News está rodando perfeitamente!", 
                Timestamp = DateTime.UtcNow 
            });
        }
    }
}