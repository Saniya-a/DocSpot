using DocSpot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DocSpot.ViewModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name is required")]
        public string Name { get; set; }

        public DepartmentVM()
        {

        }

        public DepartmentVM(Department model)
        {
            Id = model.Id;
            Name = model.Name;
           
        }

        public Department ConvertToModel(DepartmentVM model)
        {
            return new Department
            {
                Id = model.Id,
                Name = model.Name,
               
            };
        }
    }
}
