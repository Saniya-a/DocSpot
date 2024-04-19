using DocSpot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocSpot.ViewModels
{
    public class PatientVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        public string? FullName {  get; set; }

        public PatientVM()
        {
            
        }
        public PatientVM(Patient model)
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
            FullName = model.FirstName + " " + model.LastName;
        }

        public Patient ConvertToModel(PatientVM model)
        {
            return new Patient
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                Address = model.Address,
                Mobile = model.Mobile,
                DOB = (DateTime)model.DOB,
                
            };
        }
    }
}
