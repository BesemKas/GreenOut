using Microsoft.AspNetCore.Mvc;

namespace GreenOut.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
