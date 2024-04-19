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
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public bool? IsApproved { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public string? HospitalName { get; set;}
        public int? HospitalId { get; set; }
        public string? DepartmentName { get;}
        public int? DepartmentId { get; set; }


    }
}


