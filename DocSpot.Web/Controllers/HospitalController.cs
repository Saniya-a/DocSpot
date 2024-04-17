using Microsoft.AspNetCore.Mvc;

namespace DocSpot.Web.Controllers
{
    public class HospitalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult AddEdit()
        {
            return View();
        }
    }
}
