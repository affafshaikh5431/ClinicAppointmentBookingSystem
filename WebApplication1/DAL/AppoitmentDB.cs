using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicAppointmentSystem.BOL;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentSystemAPI.DAL
{
    public class AppointmentDB
    {
        private readonly ClinicAppointmentContext AppDB;

        public AppointmentDB(ClinicAppointmentContext context)
        {
            AppDB = context;
        }

        
        public async Task<IEnumerable<GetAppointmentDetails>> GetAllAppointments()
        {
            return await AppDB.Appointment
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.IsActive == true && a.IsDeleted == false)
                .OrderByDescending(a => a.AppointmentDateTime)
                 .Select(a => new GetAppointmentDetails
                 {
                     AppointmentId = a.AppointmentId,
                     DoctorName = a.Doctor.Name,
                     PatientName = a.Patient.Name,
                     AppointmentDateTime = a.AppointmentDateTime,
                     DurationInMinutes = a.DurationInMinutes,
                     Status = a.Status
                 }).ToListAsync();
        }

    


        public string AddAppointment(Appointment appointment)
        {
           
            try
            {
                appointment.IsActive = true;
                appointment.IsDeleted = false;
                appointment.CreatedOn = DateTime.Now;
                appointment.CreatedBy = "Affaf";
                appointment.Status = "Scheduled";
                AppDB.Appointment.Add(appointment);
                AppDB.SaveChanges();
                return "SUCCESS";
            }
            catch (Exception)
            {
                return "Exception";
            }
        }

       
        public string DeleteAppointment(int appointmentId)
        {
            try
            {
                var appointment = AppDB.Appointment
                    .FirstOrDefault(a => a.AppointmentId == appointmentId && a.IsActive == true && a.IsDeleted ==false);

                if (appointment == null)
                    return "Appointment Not found";

                appointment.Status = "Cancelled";
                appointment.UpdatedOn = DateTime.Now;
                appointment.UpdatedBy = "Affaf";
                AppDB.SaveChanges();
                return "SUCCESS";
            }
            catch (Exception)
            {
                return "Exception";
            }
        }

        public bool CheckIfExists( int doctorId, DateTime startTime, DateTime endTime)
        {
            if (doctorId <= 0 || startTime >= endTime)
                return true;

            var query = AppDB.Appointment
                .Where(a => a.DoctorId == doctorId
                    && a.IsActive == true
                    && a.IsDeleted == false
                    && a.AppointmentDateTime.HasValue);

            return  query.Any(a =>
                startTime < a.AppointmentDateTime.Value.AddMinutes(a.DurationInMinutes ?? 30)
                && endTime > a.AppointmentDateTime.Value
            );
        }

        
        public async Task<GetAppointmentDetails> GetAppointmentById(int appointmentId)
        {
            return await AppDB.Appointment
                .Where(a => a.AppointmentId == appointmentId
                    && a.IsActive == true
                    && a.IsDeleted == false)
                .Select(a => new GetAppointmentDetails
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDateTime = a.AppointmentDateTime,
                    DurationInMinutes = a.DurationInMinutes,
                    Status = a.Status,
                   DoctorName = a.Doctor.Name,
                   PatientName = a.Patient.Name
                })
                .FirstOrDefaultAsync();
        }

    }
}