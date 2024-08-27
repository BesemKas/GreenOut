using GreenOut.Data;
using GreenOut.Interfaces;
using GreenOut.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenOut.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductRepository _productRepository;

        public StoreController(IProductRepository productRepository)
        {
            
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAll();
            return View(products);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Product product = await _productRepository.GetByIDAsync(id);

            return PartialView("Details",product);
        }


    }
}
