using Microsoft.AspNetCore.Mvc;

namespace Aplicación_asíncrona_Web.Controllers
{
    public class controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
