using Microsoft.AspNetCore.Mvc;

namespace DocSpot.Web.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
