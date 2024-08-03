using GreenOut.Data;
using GreenOut.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOut.Controllers
{
    public class StoreController : Controller
    {
        private readonly GreenOutDbContext _context;

        public StoreController(GreenOutDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Product.Include(a=>a.Category).ToList();
            return View(products);
        }
    }
}
