using DocSpot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSpot.ViewModels
{
    public class AppointmentVM
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select doctor")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Appointment date is required")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment time is required")]
        public string AppointmentTime { get; set; }
        public bool? IsApproved { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public string? HospitalName { get; set;}
        [Range(1, int.MaxValue, ErrorMessage = "Please select hospital")]
        public int? HospitalId { get; set; }
        public string? DepartmentName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select department")]
        public int? DepartmentId { get; set; }


    }
}


