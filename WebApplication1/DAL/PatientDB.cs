
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicAppointmentSystem.BOL;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentSystemAPI.DAL
{
    public class PatientDB
    {
        private readonly ClinicAppointmentContext AppDB;

        public PatientDB(ClinicAppointmentContext context)
        {
            AppDB = context;
        }

     
        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await AppDB.Patient.Where(p => p.IsActive == true && p.IsDeleted == false).OrderBy(p => p.PatientId).ToListAsync();
        }

  
        public async Task<Patient> GetPatientByFileNo(string fileNo)
        {
             return await AppDB.Patient.Where(p => p.FileNo == fileNo&& p.IsActive == true && p.IsDeleted == false).FirstOrDefaultAsync();
        }


        public string GenerateNextFileNo(string prefix)
        {
            string year = DateTime.Now.Year.ToString();
            var lastFileNo = AppDB.Patient
                .Where(p => p.FileNo.StartsWith($"{prefix}-{year}-") && p.IsActive ==true &&p.IsDeleted==false)
                .OrderByDescending(p => p.FileNo)
                .Select(p => p.FileNo)
                .FirstOrDefault();

            int nextSequence = 1;

            if (!string.IsNullOrEmpty(lastFileNo))
            {
                var parts = lastFileNo.Split('-');
                if (parts.Length >= 3 && int.TryParse(parts[2], out int lastSeq))
                {
                    nextSequence = lastSeq + 1;
                }
            }
            string sequenceStr = nextSequence <= 9999 ? nextSequence.ToString("D4") : nextSequence.ToString();
            return $"{prefix}-{year}-{sequenceStr}";
        }




        public string AddPatient(Patient patient)
        {
            try
            {

                string fileNo = GenerateNextFileNo("PFNo");
                patient.FileNo = fileNo;
                patient.IsActive = true;
                patient.IsDeleted = false;
                patient.CreatedOn = DateTime.Now;
                patient.CreatedBy = "Affaf";
                AppDB.Patient.Add(patient);
                AppDB.SaveChanges();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return "Exception";
            }
        }

     
        public string UpdatePatient(Patient patient)
        {   
            try
            {
                var existingPatient = AppDB.Patient.FirstOrDefault(p => p.FileNo == patient.FileNo);
                if (existingPatient == null)
                    return "Patient not found";

                existingPatient.Name = patient.Name;
                existingPatient.Phone = patient.Phone;
                existingPatient.Gender = patient.Gender;
                existingPatient.UpdatedOn = DateTime.Now;
                existingPatient.UpdatedBy = "Affaf";
                AppDB.SaveChanges();

                return "SUCCESS";
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return "Exception";
            }
        }

      
        public bool PatientExistsByFileNo(string fileNo)
        {
            return AppDB.Patient.Any(p => p.FileNo == fileNo && p.IsActive == true && p.IsDeleted == false);
        }
    }
}

