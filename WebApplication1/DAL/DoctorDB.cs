
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicAppointmentSystem.BOL;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentSystemAPI.DAL
{
    public class DoctorDB
    {
        private readonly ClinicAppointmentContext AppDB;

        public DoctorDB(ClinicAppointmentContext context)
        {
            AppDB = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
                var result = await AppDB.Doctor
                .Where(d => d.IsActive == true && d.IsDeleted == false)
                .OrderBy(d => d.DoctorId)
                .ToListAsync();
                 return result;
           
        }

        public async Task<IEnumerable<DateTime>> GetAvailableTimes(int doctorId, DateTime date)
        {
            var appointments = await AppDB.Appointment
                .Where(a => a.DoctorId == doctorId
                    && a.AppointmentDateTime.HasValue
                    && a.AppointmentDateTime.Value.Date == date.Date
                    && a.IsActive == true
                    && a.IsDeleted == false && a.Status != "Cancelled")
                .Select(a => new { a.AppointmentDateTime, a.DurationInMinutes })
                .ToListAsync();

            var startHour = 10;
            var endHour = 17;
            var slotDuration = 30;
            var availableTimes = new List<DateTime>();

            for (int hour = startHour; hour < endHour; hour++)
            {
                for (int minute = 0; minute < 60; minute += slotDuration)
                {
                    var slotTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
                    var slotEndTime = slotTime.AddMinutes(slotDuration);

                    bool isAvailable = !appointments.Any(a =>
                    {
                        var appointmentStart = a.AppointmentDateTime.Value;
                        var appointmentEnd = appointmentStart.AddMinutes(a.DurationInMinutes ?? 30);
                        return slotTime < appointmentEnd && slotEndTime > appointmentStart;
                    });

                    if (isAvailable)
                    {
                        availableTimes.Add(slotTime);
                    }
                }
            }

            return availableTimes;
        }

        public async Task<Doctor> GetDoctorById(int doctorId)
        {
            return await AppDB.Doctor
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId
                    && d.IsActive == true
                    && d.IsDeleted == false);
        }
    }
}

