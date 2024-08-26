using GreenOut.Data;  // This namespace refers to classes and logic related to data access
using GreenOut.Models; // This namespace refers to the 'Account' class likely used to represent a user
using Microsoft.AspNetCore.Identity; // This namespace provides functionality for user identity management
using Microsoft.AspNetCore.Mvc; // This namespace provides functionality for working with ASP.NET MVC controllers

namespace GreenOut.Controllers // This namespace groups related controller classes within the GreenOut application
{
    public class AccountController : Controller // Inherits from the base 'Controller' class to handle user interactions
    {
        private readonly UserManager<Account> _userManager; // Injected dependency for managing user accounts
        private readonly SignInManager<Account> _signInManager; // Injected dependency for signing users in and out
        private readonly GreenOutDbContext _context; // Injected dependency for interacting with the database context

        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager, GreenOutDbContext context) // Constructor for dependency injection
        {
            _context = context;
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
                }
                return RedirectToAction("Index", "Store");
            }
            ModelState.AddModelError(string.Empty, "This Email is Taken");
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}