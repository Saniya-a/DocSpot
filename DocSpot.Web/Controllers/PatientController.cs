using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using DocSpot.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DocSpot.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IGenericRepository<Patient> _repository;
        private readonly IGenericRepository<Appointment> _appointment;
        private readonly IGenericRepository<Hospital> _hospitalRepo;
        private readonly IGenericRepository<Department> _departmentRepo;

        public PatientController(IGenericRepository<Patient> repository, IGenericRepository<Appointment> appointment, 
                                    IGenericRepository<Hospital> hospitalRepo, IGenericRepository<Department> departmentRepo)
        {
            _repository = repository;
            _appointment = appointment;
            _hospitalRepo = hospitalRepo;
            _departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            try
            {
                Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>> orderBy = query =>
                {
                    return query.OrderBy(item => item.AppointmentDate).ThenBy(x => x.AppointmentTime);
                };

                var data = _appointment.GetAll(null, orderBy);
                var patientList = new List<PatientVM>();
                //foreach (var item in data)
                //{
                //    patientList.Add(new PatientVM(item));
                //}
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IActionResult> AddEdit(int id)
        {
            try
            {
                if (id == 0)
                {
                    return View(new PatientVM());
                }
                else
                {
                    var editObj = await _repository.GetById(id);
                    var model = new PatientVM(editObj);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(PatientVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        var add = model.ConvertToModel(model);
                        await _repository.Add(add);
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        var add = model.ConvertToModel(model);
                        await _repository.Update(add);
                        return RedirectToAction("GetAll");
                    }

                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> AddEditAppointment(int id)
        {
            ViewBag.HospitalList = await _hospitalRepo.Read();
            ViewBag.DepartmentList = await _departmentRepo.Read();
            ViewBag.AppointmentTimeList = Timeslot.AvailableTimeSlots;
            try
            {
                if (id == 0)
                {
                    return View(new AppointmentVM());
                }
                else
                {
                    var editObj = await _repository.GetById(id);
                    var model = new PatientVM(editObj);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> GetDoctorList(int hospitalId, int departmentId)
        {
            var doctors = await _repository.Read();
            return Json(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchString = Request.Form["search[value]"].FirstOrDefault();
                Expression<Func<Patient, bool>> filter = null;// Filter expression
                if (!string.IsNullOrWhiteSpace(searchString))
                    filter = x => (x.FirstName.Contains(searchString) || x.LastName.Contains(searchString) || 
                        x.Username.Contains(searchString) || x.Address.Contains(searchString) || x.Mobile.Contains(searchString));

                Func<IQueryable<Patient>, IOrderedQueryable<Patient>> orderBy = query =>
                {
                    return query.OrderBy(item => item.LastName).ThenBy(x => x.FirstName);
                };

                var data = _repository.GetAll(Convert.ToInt32(start), Convert.ToInt32(length), filter, orderBy);
                var patientList = new List<PatientVM>();
                foreach (var item in data.items)
                {
                    patientList.Add(new PatientVM(item));
                }
                return Json(new { draw = draw, recordsTotal = data.total, recordsFiltered = data.items.Count(), data = patientList });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
