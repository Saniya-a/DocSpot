using AppAPI.Auth;
using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DocSpot.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IGenericRepository<Department> _repository;
        public DepartmentController(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }



        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Department>> GetAll()
        {
            try
            {
                var patient = await _repository.Read();
                if (patient == null)
                {
                    throw new KeyNotFoundException("Couldn't find the id");
                }
                else
                {

                    return patient;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpPost]
        [Route("Add")]

        public async Task<IActionResult> Add(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var add = model.ConvertToModel(model);
                    await _repository.Add(add);
                    return Ok(new Response { Status = "Success", Message = "Department created successfully!" });


                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("Edit")]

        public async Task<IActionResult> Edit(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var add = model.ConvertToModel(model);
                    await _repository.Update(add);
                    return Ok(new Response { Status = "Success", Message = "Department updated successfully!" });


                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {

            var deleteObj = await _repository.GetById(id);
            await _repository.Delete(deleteObj);
            return Ok(new Response { Status = "Success", Message = "Item Deleted successfully!" });
        }
        [HttpPost]
        [Route("LoadData")]
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

                return Ok(new { draw = draw, recordsTotal = data.total, recordsFiltered = data.total, data = data.items });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
