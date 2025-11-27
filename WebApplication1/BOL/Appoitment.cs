using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAppointmentSystem.BOL
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int PatientId { get; set; }

        public DateTime? AppointmentDateTime { get; set; }
        public int? DurationInMinutes { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public bool? IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; } = false;

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
