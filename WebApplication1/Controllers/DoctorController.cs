using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicAppointmentSystem.BOL;
using ClinicAppointmentSystemAPI.BLL;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorBS _doctorBS;

        public DoctorController(DoctorBS doctorBS)
        {
            _doctorBS = doctorBS;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorBS.GetAllDoctors();
            return Ok(doctors);
        }

       
        [HttpGet("{doctorId}/availabletimes")]
        public async Task<IActionResult> GetAvailableTimes(int doctorId, [FromQuery] DateTime date)
        {
            var result = await _doctorBS.GetAvailableTimes(doctorId, date);

            if (!result.isValid)
                return BadRequest(result.errorMessage);

            return Ok(result.availableTimes);
        }
    }
}
