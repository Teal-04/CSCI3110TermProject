using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CSCI3110_Term_Project.Data;
using CSCI3110_Term_Project.Models;

namespace CSCI3110_Term_Project.Controllers
{
    public class BudgetController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BudgetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Budget
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var budgets = _context.Budgets
                .Include(b => b.Category)
                .Where(b => b.UserId == userId.Value)
                .ToList();

            return View(budgets);
        }

        // GET: /Budget/Details/5
        public IActionResult Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var budget = _context.Budgets
                .Include(b => b.Category)
                .FirstOrDefault(b => b.BudgetId == id && b.UserId == userId.Value);

            if (budget == null)
                return NotFound();

            return View(budget);
        }

        // GET: /Budget/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: /Budget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Budget budget)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            // DEBUG: collect any model‐binding errors
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                ViewBag.Categories = _context.Categories.ToList();
                return View(budget);
            }

            // DEBUG: we got a valid model—let's confirm the values
            Console.WriteLine($"Creating budget: User={userId}, Cat={budget.CategoryId}, Amt={budget.Amount}");

            budget.UserId = userId.Value;
            _context.Budgets.Add(budget);
            _context.SaveChanges();

            Console.WriteLine("Saved OK, new id = " + budget.BudgetId);
            return RedirectToAction(nameof(Index));
        }


        // GET: /Budget/Edit/5
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var budget = _context.Budgets.Find(id);
            if (budget == null || budget.UserId != userId.Value)
                return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(budget);
        }

        // POST: /Budget/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Budget formBudget)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(formBudget);
            }

            var budget = _context.Budgets.Find(id);
            if (budget == null || budget.UserId != userId.Value)
                return NotFound();

            // update only allowed fields
            budget.Amount = formBudget.Amount;
            budget.CategoryId = formBudget.CategoryId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Budget/Delete/5
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var budget = _context.Budgets
                .Include(b => b.Category)
                .FirstOrDefault(b => b.BudgetId == id && b.UserId == userId.Value);

            if (budget == null)
                return NotFound();

            return View(budget);
        }

        // POST: /Budget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var budget = _context.Budgets.Find(id);
            if (budget == null || budget.UserId != userId.Value)
                return NotFound();

            _context.Budgets.Remove(budget);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
