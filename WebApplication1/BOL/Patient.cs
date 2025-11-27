using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicAppointmentSystem.BOL
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Phone { get; set; }


        public string FileNo { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        public bool? IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; } = false;

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}

