using DocSpot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocSpot.ViewModels
{
    public class DoctorVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Fees is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Fees must be a positive value")]
        public decimal Fees { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select department")]
        public int DepartmentId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select hospital")]
        public int HospitalId { get; set; }

        public string DepartmentName { get; set; }

        public string HospitalName { get; set; }
        public string? FullName { get; set; }

        public DoctorVM()
        {

        }

        public DoctorVM(Doctor model)
        {
            Id = model.Id;
            FullName = model.FirstName + " " + model.LastName;  
            FirstName = model.FirstName;
            LastName = model.LastName;
            Username = model.Username;
            Password = model.Password;
            Email = model.Email;
            Address = model.Address;
            Mobile = model.Mobile;
            DOB = model.DOB;
            Fees = model.Fees;
            DepartmentId = model.DepartmentId;
            HospitalId = model.HospitalId;
            DepartmentName = model.Department.Name;
            HospitalName = model.Hospital.Name;
        }

        public Doctor ConvertToModel(DoctorVM model)
        {
            return new Doctor
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                Address = model.Address,
                Mobile = model.Mobile,
                DOB = model.DOB,
                Fees = model.Fees,
                DepartmentId = model.DepartmentId,
                HospitalId = model.HospitalId,
            };
        }
    }
}

