using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Linq.Expressions;
using static DocSpot.Web.Filters.AutherizationFilter;

namespace DocSpot.Web.Controllers
{
    [AdminAuthFilter]
    public class DepartmentController : Controller
    {
        private readonly IGenericRepository<Department> _repository;
        public DepartmentController(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            return View();
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            try
            {
                if (id == 0)
                {
                    return View(new DepartmentVM());
                }
                else
                {
                    var editObj = await _repository.GetById(id);
                    var model = new DepartmentVM(editObj);
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
        public async Task<IActionResult> AddEdit(DepartmentVM model)
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
                Expression<Func<Department, bool>> filter = null;// Filter expression
                if (!string.IsNullOrWhiteSpace(searchString))
                    filter = x => (x.Name.Contains(searchString));

                Func<IQueryable<Department>, IOrderedQueryable<Department>> orderBy = query =>
                {
                    return query.OrderBy(item => item.Name);
                };

                var data = _repository.GetAll(Convert.ToInt32(start), Convert.ToInt32(length), filter, orderBy);

                return Json(new { draw = draw, recordsTotal = data.total, recordsFiltered = data.total, data = data.items });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
