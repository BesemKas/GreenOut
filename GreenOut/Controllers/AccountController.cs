
using GreenOut.Data;  
using GreenOut.Interfaces;
using GreenOut.Models; 
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NuGet.Protocol.Plugins;
using System.Security.Claims; 

namespace GreenOut.Controllers 
{
    public class AccountController : Controller // Inherits from the base 'Controller' class to handle user interactions
    {
        private readonly UserManager<Account> _userManager; // Injected dependency for managing user accounts
        private readonly SignInManager<Account> _signInManager; // Injected dependency for signing users in and out

        private readonly IAccountRepository _accountRepository;

        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager, IAccountRepository accountRepository) // Constructor for dependency injection
        {

            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Login() // GET request handler for the login view
        {
            var responseValues = new LoginViewModel(); // Creates a new instance of the view model class
            return View(responseValues); // Returns the login view with an empty view model
        }

        [HttpPost] // Decorator to specify this method handles POST requests
        public async Task<IActionResult> Login(LoginViewModel viewmodel) // Handles login form submission
        {
            if (!ModelState.IsValid) // Checks if the form data is valid
            {
                return View(viewmodel); // Returns the login view with the same view model for re-submission
            }

            var account = await _userManager.FindByEmailAsync(viewmodel.EmailAddress); // Attempts to find an account by the email address from the view model

            if (account != null) // If an account is found with the provided email
            {
                var checkPW = await _userManager.CheckPasswordAsync(account, viewmodel.Password); // Checks if the password from the view model matches the account's password

                if (checkPW) // If the password matches
                {
                    var result = await _signInManager.PasswordSignInAsync(account, viewmodel.Password, false, false); // Signs the user in with the provided credentials

                    if (result.Succeeded) // If sign-in was successful
                    {
                        return RedirectToAction("Index", "Store"); // Redirects the user to the 'Store' controller's 'Index' action method
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Credentials"); // Adds a model error message for invalid credentials
                    return View(viewmodel); // Returns the login view with the view model for re-submission
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Credentials"); // Adds a model error message for invalid credentials
            return View(viewmodel); // Returns the login view with the view model for re-submission
        }

        public IActionResult Register() // GET request handler for the Register view
        {
            var responseValues = new RegisterViewModel(); // Creates a new instance of the view model class
            return View(responseValues); // Returns the Register view with an empty view model
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var account = await _userManager.FindByEmailAsync(viewModel.EmailAddress);
            if (account == null)
            {
               var newAccount = new Account()
                {
                    Email = viewModel.EmailAddress,
                    UserName = viewModel.EmailAddress,
                    EmailConfirmed = false,
                    Name = viewModel.Name,
                    Surname = viewModel.Surname,
                    Address = viewModel.Address,
                    PhoneNumber = viewModel.PhoneNumber,

                };
                var result = await _userManager.CreateAsync(newAccount, viewModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newAccount, UserRoles.User);
                    var cart = new ShoppingCart()
                    {
                        AccountID = newAccount.Id,
                    };
                    _accountRepository.CreateCart(cart);
                }
                return RedirectToAction("Index", "Store");
            }
            ModelState.AddModelError(string.Empty, "This Email is Taken");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Store");
        }


        public  async Task<IActionResult> Cart()
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart =  _accountRepository.GetCartByAccountIdNoTracking(accountId);

            var viewmodel = new CartViewModel
            {
                Cart = cart,
                CartItems =  _accountRepository.GetAllCartItems(cart.CartID)
            };
            return View(viewmodel);
        }


        public  async Task<IActionResult> AddToCart(int id)
        {
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart =  _accountRepository.GetCartByAccountIdNoTracking(accountId);

            var item = new CartItem()
            {
                CartID = cart.CartID,
                ProductID = id,
                Quantity = 1
            };

            _accountRepository.AddtoCart(item);



            return RedirectToAction("Cart", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            
            var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _accountRepository.GetCartByAccountIdNoTracking(accountId);
            var cartItems = _accountRepository.GetAllCartItems(cart.CartID).ToList();

            var order = new Order()
            {
                OrderDate = DateTime.Now,
                AccountID = accountId
            };

            _accountRepository.CreateOrder(order);

            foreach (var item in cartItems)
            {
                var product = item.Product;
                var orderItem = new OrderItem()
                {
                    OrderID = order.OrderID,
                    ProductID = product.ProductID,
                    Quantity = item.Quantity,
                };
                await _accountRepository.AddtoOrder(orderItem);
            }
            foreach (var item in cartItems)
            {
                _accountRepository.Delete(item);
            }



            return RedirectToAction("Index", "Store");
        }

        public IActionResult Delete(int id)
        {
            var cartItem = _accountRepository.GetCartItemByIDAsyncNoTracking(id);

            if (cartItem != null)
            {
                _accountRepository.Delete(cartItem);
                return RedirectToAction("Cart", "Account");
            }
            return View("Cart", "Account");
        }



    }
}