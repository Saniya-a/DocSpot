using DocSpot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocSpot.ViewModels
{
    public class HospitalVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact must be 10 digits")]
        public string Contact { get; set; }



        public HospitalVM()
        {
            
        }

        public HospitalVM(Hospital model)
        {
            Id = model.Id;
            Name = model.Name;
            Address = model.Address;
            Contact = model.Contact;
        }

        public Hospital ConvertToModel(HospitalVM hospitalVM)
        {
            return new Hospital
            {
                Id = hospitalVM.Id,
                Name = hospitalVM.Name,
                Address = hospitalVM.Address,
                Contact = hospitalVM.Contact,
            };
        }
    }
}
