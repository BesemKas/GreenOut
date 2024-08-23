using Microsoft.AspNetCore.Mvc;

namespace GreenOut.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login
            ()
        {
            return View();
        }
    }
}
