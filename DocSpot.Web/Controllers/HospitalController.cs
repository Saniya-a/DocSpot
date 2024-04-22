using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using static DocSpot.Web.Filters.AutherizationFilter;

namespace DocSpot.Web.Controllers
{
    [AdminAuthFilter]
    public class HospitalController : Controller
    {
        private readonly IGenericRepository<Hospital> _repository;
        public HospitalController(IGenericRepository<Hospital> repository)
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
            if (id == 0)
            {
                return View(new HospitalVM());
            }
            else
            {
                var editObj = await _repository.GetById(id);
                var model = new HospitalVM(editObj);
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(HospitalVM model)
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
            var model = new HospitalVM(deleteObj);
            //await _repository.Delete(deleteObj);
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
                Expression<Func<Hospital, bool>> filter = null;// Filter expression
                if (!string.IsNullOrWhiteSpace(searchString))
                    filter = x => (x.Name.Contains(searchString) || x.Contact.Contains(searchString) || x.Address.Contains(searchString));

                Func<IQueryable<Hospital>, IOrderedQueryable<Hospital>> orderBy = query =>
                {
                    return query.OrderBy(item => item.Name);
                };

                var data = _repository.GetAll(Convert.ToInt32(start), Convert.ToInt32(length), filter, orderBy);

                return Json(new { draw = draw, recordsTotal = data.total, recordsFiltered = data.items.Count(), data = data.items });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
