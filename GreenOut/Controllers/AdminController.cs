using GreenOut.Data;
using GreenOut.Interfaces;
using GreenOut.Models;
using GreenOut.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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
            var viewModel = new ProductViewModel
            {
                Product = new Product(), // Create a new Product instance
                Categories = _adminRepository.GetAllCategories(),
                

            };


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)

            {
                // Category existence check
                var categoryExists = await _adminRepository.CategoryExists(viewModel.Product.CategoryID);
                if (!categoryExists)
                {
                    ModelState.AddModelError("Product.CategoryID", "Invalid CategoryID. Please select a valid category.");
                }

                

                if (ModelState.IsValid)
                {
                    object value = _adminRepository.Add(viewModel.Product);
                    return RedirectToAction("Inventory");
                }
            }

            // Repopulate categories if validation fails
            viewModel.Categories = _adminRepository.GetAllCategories();

            return View(viewModel);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _adminRepository.GetProductByIDAsync(id);
            if (product == null) return View("Error");
            ViewBag.Categories = _adminRepository.GetAllCategories();
            var viewModel = new ProductEditViewModel
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Stock = product.Stock,
                CategoryID = product.CategoryID,


            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ProductEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", viewModel);
            }


            var productEdit = await _adminRepository.GetProductByIDAsyncNoTracking (id); //stop tracking errors
            var productUpdate = new Product
            {
                ProductID = id,
                Name = viewModel.Name,
                Price = viewModel.Price,
                Description = viewModel.Description,
                ImageURL = viewModel.ImageURL,
                Stock = viewModel.Stock,
                CategoryID = viewModel.CategoryID,

            };


            _adminRepository.Update(productUpdate);
            return RedirectToAction("Inventory");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _adminRepository.GetProductByIDAsyncNoTracking(id);
            if (product != null)
            {
                _adminRepository.Delete(product);
                return RedirectToAction("Inventory","Admin");
            }
            return View("Inventory","Admin");
        }

        public async Task<IActionResult> Preview(int id)
        {
            Product product = await _adminRepository.GetProductByIDAsync(id);

            return PartialView("Details", product);
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
