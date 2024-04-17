using Microsoft.AspNetCore.Mvc;

namespace DocSpot.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
