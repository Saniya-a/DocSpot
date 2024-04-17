using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSpot.ViewModels
{
    public class DoctorVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public decimal Fees { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
