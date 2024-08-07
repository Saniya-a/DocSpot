﻿using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using DocSpot.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using static DocSpot.Web.Filters.AutherizationFilter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DocSpot.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IGenericRepository<Patient> _repository;
        private readonly IGenericRepository<Appointment> _appointment;
        private readonly IGenericRepository<Hospital> _hospitalRepo;
        private readonly IGenericRepository<Department> _departmentRepo;
        private readonly IAppointmentRepository _appointmentRepo;
        public PatientController(IGenericRepository<Patient> repository, IGenericRepository<Appointment> appointment, 
                                    IGenericRepository<Hospital> hospitalRepo, IGenericRepository<Department> departmentRepo, 
                                    IAppointmentRepository appointmentRepo)
        {
            _repository = repository;
            _appointment = appointment;
            _hospitalRepo = hospitalRepo;
            _departmentRepo = departmentRepo;
            _appointmentRepo = appointmentRepo;
        }

        [PatientAuthFilter]
        public async Task<IActionResult> Index()
        {
            var patientId = HttpContext.Session.GetInt32("PatientId") ?? 0;
            var list = await _appointmentRepo.GetAppointmentsByPatientId(patientId);
            return View(list);
        }
        [PatientAuthFilter]
        public async Task<IActionResult> ViewProfile()
        {
            try
            {
                var patientId = HttpContext.Session.GetInt32("PatientId") ?? 0;
                var details = await _repository.GetById(patientId);
                var model = new PatientVM(details);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Some error occured");
                return View("Index");
            }
        }
        [PatientAuthFilter]
        public async Task<IActionResult> UpdateProfile()
        {
            try
            {
                var patientId = HttpContext.Session.GetInt32("PatientId") ?? 0;
                var details = await _repository.GetById(patientId);
                var model = new PatientVM(details);
                return View(model);
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Some error occured");
                return RedirectToAction("ViewProfile");
            }
        }

        [PatientAuthFilter]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(PatientVM model)
        {
            try
            {
                var add = model.ConvertToModel(model);
                await _repository.Update(add);
                return RedirectToAction("Index", "Patient");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Some error occured");
                return View(model);
            }
        }

        [AdminAuthFilter]
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

        [AdminAuthFilter]
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

        [AdminAuthFilter]
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

        [PatientAuthFilter]
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
                    var editObj = await _appointmentRepo.GetById(id);
                    
                    return View(editObj);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [PatientAuthFilter]
        [HttpPost]
        public async Task<IActionResult> AddEditAppointment(AppointmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        model.PatientId = HttpContext.Session.GetInt32("PatientId") ?? 0;
                        await _appointmentRepo.Add(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        
                        await _appointmentRepo.Update(model);
                        return RedirectToAction("Index");
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
        [AdminAuthFilter]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteObj = await _repository.GetById(id);
            await _repository.Delete(deleteObj);
            return Ok("Item deleted successfully");
        }
        [AdminAuthFilter]
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
