using Microsoft.AspNetCore.Mvc;
using ClinicAppointmentSystemAPI.BLL;
using ClinicAppointmentSystem.BOL;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ClinicAppointmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientBS _patientBS;

        public PatientsController(PatientBS patientBS)
        {
            _patientBS = patientBS;
        }

      
        // GET: /api/patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _patientBS.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("{fileNo}")]
        public async Task<ActionResult<Patient>> GetPatientByFileNo(string fileNo)
        {
            if (string.IsNullOrWhiteSpace(fileNo))
                return BadRequest("FileNo is required");

            var patient = await _patientBS.GetPatientByFileNo(fileNo);

            if (patient == null)
                return NotFound(new { message = "Patient not found" });

            return Ok(patient);
        }


        [HttpPost("add")]
        public ActionResult<string> AddPatient([FromBody] Patient patient)
        {
            try
            {

                if (patient == null)
                    return BadRequest("Patient is null");

                var result = _patientBS.AddPatient(patient);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("update")]
        public ActionResult<string> UpdatePatient([FromBody] Patient patient)
        {
            if (patient == null)
                return BadRequest("Patient cannot be null");

            if (!_patientBS.PatientExistsByFileNo(patient.FileNo))
                return NotFound("Patient not found");

            var result = _patientBS.UpdatePatient(patient);
            return Ok(result);
        }
    }
}
