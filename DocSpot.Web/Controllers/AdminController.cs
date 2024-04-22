using DocSpot.Models;
using DocSpot.Repository.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using static DocSpot.Web.Filters.AutherizationFilter;

namespace DocSpot.Web.Controllers
{
    [AdminAuthFilter]
    public class AdminController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepo;
        public AdminController(IAppointmentRepository appointmentRepo)
        {
           
            _appointmentRepo = appointmentRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllAppointments()
        {
            var list = await _appointmentRepo.GetAll();
            return View(list);
        }
    }
}
