using DocSpot.Models;
using System;
using System.Collections.Generic;
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
        public int HospitalId { get; set; }

        public DoctorVM()
        {

        }

        public DoctorVM(Doctor model)
        {
            Id = model.Id;
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

