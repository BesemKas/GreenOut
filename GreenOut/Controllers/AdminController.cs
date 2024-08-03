using GreenOut.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOut.Controllers
{
    public class AdminController : Controller
    {
        private readonly GreenOutDbContext _context;

        public AdminController(GreenOutDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() ///dashboard
        {
            return View();
        }

        public IActionResult Inventory() ///Inventory management
        {
            var products = _context.Product.Include(a => a.Category).ToList();
            return View(products);
        }

        public IActionResult Customer() ///Customer management
        {
            var accounts = _context.Account.ToList();
            return View(accounts);
        }

        public IActionResult Order() ///order management
        {
            var orders = _context.Order.ToList();
            return View(orders);
        }

        public IActionResult Payment() ///payment management
        {
            return View();
        }
    }
}
