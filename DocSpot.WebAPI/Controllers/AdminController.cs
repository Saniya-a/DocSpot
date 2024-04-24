using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using DocSpot.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocSpot.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IGenericRepository<Patient> _genericPatient;
        private readonly IGenericRepository<Department> _genericDept;

        public AdminController(IGenericRepository<Patient> genericPatient, IGenericRepository<Department> genericDept)
        {
            _genericPatient = genericPatient;
            _genericDept = genericDept;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IResult> AddEdit([FromBody] PatientVM model)
        {
            try
            {
                var patient = model.ConvertToModel(model);
                if (model.Id == 0)
                {
                    await _genericPatient.Add(patient);
                    return Results.Ok();
                }
                else
                {
                    await _genericPatient.Update(patient);
                    return Results.Ok();
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("Get")]
        public async Task<PatientVM> Get(int id)
        {
            try
            {
                var patient = await _genericPatient.GetById(id);
                if (patient == null)
                {
                    throw new KeyNotFoundException("Couldn't find the id");
                }
                else
                {
                    var patientVM = new PatientVM(patient);
                    return patientVM;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Department>> GetAll()
        {
            try
            {
                var patient = await _genericDept.Read();
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



    }
}
