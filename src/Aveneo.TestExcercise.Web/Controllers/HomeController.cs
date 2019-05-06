using Microsoft.AspNetCore.Mvc;

namespace Aveneo.TestExcercise.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}