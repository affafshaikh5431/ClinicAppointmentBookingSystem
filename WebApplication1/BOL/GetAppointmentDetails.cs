using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.BOL
{
    public class GetAppointmentDetails
    {
       
            public int AppointmentId { get; set; }
            public string DoctorName { get; set; }
            public string PatientName { get; set; }
            public DateTime? AppointmentDateTime { get; set; }
            public int? DurationInMinutes { get; set; }
            public string Status { get; set; }
            public bool? IsActive { get; set; } = true;
            public bool? IsDeleted { get; set; } = false;
            public string CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedOn { get; set; }

    }
  
}
