using GreenOut.Data;
using GreenOut.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOut.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
          _adminRepository = adminRepository;
        }

        public IActionResult Index() ///Admin dashboard
        {
            return View();
        }

        public async Task<IActionResult> Inventory() ///Inventory management
        {
            var products = await _adminRepository.GetAllProducts(); 
            return View(products);
        }

        public async Task<IActionResult> Customer() ///Customer management
        {
            var accounts = await _adminRepository.GetAllAccounts();
            return View(accounts);
        }

        public async Task<IActionResult> Order() ///order management
        {
            var orders = await _adminRepository.GetAllOrders();
            return View(orders);
        }

        public async Task<IActionResult> Payment() ///payment management
        {
            var payments = await _adminRepository.GetAllPayments();
            return View(payments);
        }
    }
}
