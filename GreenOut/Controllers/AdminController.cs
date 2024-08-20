using GreenOut.Data;
using GreenOut.Interfaces;
using GreenOut.Models;
using GreenOut.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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



        public IActionResult Create()
        {
            var categories = _adminRepository.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Value", "Text");

            var product = new Product();
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Customer() ///Customer management
        {
            var accounts = _adminRepository.GetAllAccounts();
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
