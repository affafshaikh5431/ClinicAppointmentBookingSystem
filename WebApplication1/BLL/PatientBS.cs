using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicAppointmentSystem.BOL;
using ClinicAppointmentSystemAPI.DAL;

namespace ClinicAppointmentSystemAPI.BLL
{
    public class PatientBS
    {
        private readonly PatientDB _objPatientDB;

        public PatientBS(PatientDB patientDB)
        {
            _objPatientDB = patientDB;
        }

    
        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            var patients = await _objPatientDB.GetAllPatients();

            if (patients == null)
                return new List<Patient>(); 

            return patients;
        }

     
        public async Task<Patient> GetPatientByFileNo(string fileNo)
        {
            if (string.IsNullOrWhiteSpace(fileNo))
                return null;

            var patient = await _objPatientDB.GetPatientByFileNo(fileNo);

            return patient; 
        }

      
        public string AddPatient(Patient patient)
        {
            if (patient == null)
                return "Patient Cannot be null";

            if (PatientExistsByFileNo(patient.FileNo))
                return "Patient with this FileNo already exists";

            return _objPatientDB.AddPatient(patient); 
        }

        
        public string UpdatePatient(Patient patient)
        {
            if (patient == null)
                return "Patient Cannot be null";

            return _objPatientDB.UpdatePatient(patient); 
        }

       
        public bool PatientExistsByFileNo(string fileNo)
        {
            if (string.IsNullOrWhiteSpace(fileNo))
                return false;

            return _objPatientDB.PatientExistsByFileNo(fileNo);
        }
    }
}
