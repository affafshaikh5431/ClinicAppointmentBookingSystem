using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicAppointmentSystem.BOL;
using ClinicAppointmentSystemAPI.DAL;

namespace ClinicAppointmentSystemAPI.BLL
{
    public class DoctorBS
    {
        private readonly DoctorDB _objDoctorDB;

        public DoctorBS(DoctorDB doctorDB)
        {
            _objDoctorDB = doctorDB;
        }

      
        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            var doctors = await _objDoctorDB.GetAllDoctors();

            if (doctors == null)
            {
                return new List<Doctor>(); 
            }

            return doctors;
        }

        public async Task<(bool isValid, string errorMessage, IEnumerable<DateTime> availableTimes)> GetAvailableTimes(int doctorId, DateTime date)
        {
          
            if (doctorId <= 0)
                return (false, "Invalid Doctor ID", null);

            if (date == default ||  date == null )
                return (false, "Invalid Date", null);

            var doctor = await _objDoctorDB.GetDoctorById(doctorId);
            if (doctor == null)
                return (false, "Doctor Not found", null);

            
            var availableTimes = await _objDoctorDB.GetAvailableTimes(doctorId, date);
            if (availableTimes == null)
                availableTimes = new List<DateTime>();

            return (true, null, availableTimes);
        }
    

 
    public async Task<Doctor> GetDoctorById(int doctorId)
        {
            var doctor = await _objDoctorDB.GetDoctorById(doctorId);

            if (doctor == null)
            {
                return null; 
            }

            return doctor;
        }
    }
}
