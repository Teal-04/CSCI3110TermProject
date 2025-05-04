using Microsoft.AspNetCore.Mvc;
using CSCI3110_Term_Project.Data;
using CSCI3110_Term_Project.Models;
using CSCI3110_Term_Project.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CSCI3110_Term_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
            => _context = context;

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST: /Account/Register
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (vm.Password != vm.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View(vm);
            }

            if (_context.Users.Any(u => u.Email == vm.Email))
            {
                ModelState.AddModelError("", "Email already in use.");
                return View(vm);
            }

            var user = new User
            {
                Name = vm.Name,
                Email = vm.Email,
                // In a real app you'd hash this!
                PasswordHash = vm.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Log the user in
            HttpContext.Session.SetInt32("UserId", user.UserId);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: /Account/Login
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = _context.Users
                .FirstOrDefault(u => u.Email == vm.Email
                                  && u.PasswordHash == vm.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(vm);
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
