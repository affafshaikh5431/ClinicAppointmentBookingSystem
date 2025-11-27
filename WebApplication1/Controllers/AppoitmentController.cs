using Microsoft.AspNetCore.Mvc;
using ClinicAppointmentSystemAPI.BLL;
using ClinicAppointmentSystem.BOL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicAppointmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentBS _appointmentBS;

        public AppointmentsController(AppointmentBS appointmentBS)
        {
            _appointmentBS = appointmentBS;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentBS.GetAllAppointments();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAppointmentDetails>> GetAppointmentById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid Appointment ID" });

            var appointment = await _appointmentBS.GetAppointmentById(id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });

            return Ok(appointment);
        }

        [HttpPost("add")]
        public ActionResult AddAppointment([FromBody] Appointment appointment)
        {
            var result = _appointmentBS.AddAppointment(appointment); 
            if (result.StartsWith("SUCCESS"))
            {
                return Ok(new
                {
                    message = "Appointment Booked successfully",
                    appointmentId = appointment.AppointmentId
                }) ;
            }

            return BadRequest(new { message = result });
        }


        [HttpPut("cancel/{id}")]
        public ActionResult CancelAppointment(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid Appointment ID" });

            var result = _appointmentBS.CancelAppointment(id);

            if (result == "SUCCESS")
                return Ok(new { message = "Appointment cancelled successfully" });

            return BadRequest(new { message = result });
        }
    }
}
