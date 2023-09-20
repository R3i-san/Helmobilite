using Microsoft.AspNetCore.Mvc;

namespace ProjetWeb.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 3600)]
        public IActionResult Index()
        {
            return View();
        }
    }
}