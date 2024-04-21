using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Linq.Expressions;

namespace DocSpot.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IGenericRepository<Doctor> _repository;
        private readonly IGenericRepository<Hospital> _hospitalRepo;
        private readonly IGenericRepository<Department> _departmentRepo;
        private readonly IAppointmentRepository _appointmentRepo;

        public DoctorController(IGenericRepository<Doctor> repository, IGenericRepository<Hospital> hospitalRepo, 
            IGenericRepository<Department> departmentRepo, IAppointmentRepository appointmentRepo)
        {
            _repository = repository;
            _hospitalRepo = hospitalRepo;
            _departmentRepo = departmentRepo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<IActionResult> Index()
        {
            var doctorId = 4;
            var list = await _appointmentRepo.GetAppointmentsByDoctorId(doctorId);
            return View(list);
        }

        public async Task<IActionResult> ApproveAppointment(int id)
        {
            
            await _appointmentRepo.ApproveAppointment(id);
            return RedirectToAction("Index");
        }

        public IActionResult GetAll()
        {

            return View();
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.HospitalList = await _hospitalRepo.Read();
            ViewBag.DepartmentList = await _departmentRepo.Read();

            if (id == 0)
            {
                return View(new DoctorVM());
            }
            else
            {
                var editObj = await _repository.GetById(id);
                var model = new DoctorVM(editObj);
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(DoctorVM model)
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

        public async Task<IActionResult> Delete(int id)
        {

            var deleteObj = await _repository.GetById(id);
            var model = new DoctorVM(deleteObj);
            await _repository.Delete(deleteObj);
            return Ok("Item deleted successfully");
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
                Expression<Func<Doctor, bool>> filter = null;// Filter expression
                if (!string.IsNullOrWhiteSpace(searchString))
                    filter = x => (x.FirstName.Contains(searchString) || x.LastName.Contains(searchString) || x.Address.Contains(searchString));

                Func<IQueryable<Doctor>, IOrderedQueryable<Doctor>> orderBy = query =>
                {
                    return query.OrderBy(item => item.FirstName);
                };
                var includeProperties = "Department,Hospital";
                var data = _repository.GetAll(Convert.ToInt32(start), Convert.ToInt32(length), filter, orderBy, includeProperties);
                var list = new List<DoctorVM>();
                foreach (var item in data.items)
                {
                    list.Add(new DoctorVM(item));
                }
                return Json(new { draw = draw, recordsTotal = data.total, recordsFiltered = data.items.Count(), data = list });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        public async Task<IActionResult> GetDoctorList(int hospitalId, int departmentId)
        {
            //var doctorList = _repository.Read().Where(x => x.HospitalId == hospitalId && x.DepartmentId == departmentId).ToList();
            Expression<Func<Doctor, bool>> filter = x => x.HospitalId == hospitalId && x.DepartmentId == departmentId;
            Func<IQueryable<Doctor>, IOrderedQueryable<Doctor>> orderBy = query =>
            {
                return query.OrderBy(item => item.LastName).ThenBy(itm => itm.FirstName);
            };
            var doctors = _repository.GetAll(filter, orderBy);
            return Json(doctors);
        }


    }
}
