using ClinicAppointmentSystem.BOL;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ClinicAppointmentContext _context;

        public TestController(ClinicAppointmentContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            if (_context.Database.CanConnect())
                return Ok("Database connected!");
            return StatusCode(500, "Cannot connect to database.");
        }
    }
}
