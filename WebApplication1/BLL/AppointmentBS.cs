using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicAppointmentSystem.BOL;
using ClinicAppointmentSystemAPI.DAL;

namespace ClinicAppointmentSystemAPI.BLL
{
    public class AppointmentBS
    {
        private readonly AppointmentDB _objAppointmentDB;

        public AppointmentBS(AppointmentDB appointmentDB)
        {
            _objAppointmentDB = appointmentDB;
        }

 
        public async Task<IEnumerable<GetAppointmentDetails>> GetAllAppointments()
        {
            var appointments = await _objAppointmentDB.GetAllAppointments();
            return appointments ?? new List<GetAppointmentDetails>();
        }

        public async Task<GetAppointmentDetails> GetAppointmentById(int appointmentId)
        {
            if (appointmentId <= 0)
                return null;

            return await _objAppointmentDB.GetAppointmentById(appointmentId);
        }

    
        public string AddAppointment(Appointment appointment)
        {
            if (appointment == null)
                return "Appointment Cannot be null";

            if (!appointment.AppointmentDateTime.HasValue)
                return "Appointment Date/Time is required";

            bool isOverlapping = _objAppointmentDB.CheckIfExists( appointment.DoctorId, appointment.AppointmentDateTime.Value,appointment.AppointmentDateTime.Value.AddMinutes(appointment.DurationInMinutes ?? 30));

            if (isOverlapping)
                return "Doctor Already has an Appointment in this time slot";


            return _objAppointmentDB.AddAppointment(appointment);
        }

       
        public string CancelAppointment(int appointmentId)
        {
            if (appointmentId <= 0)
                return "Invalid Appointment ID";

            return _objAppointmentDB.DeleteAppointment(appointmentId);
            
        }
    }
}
