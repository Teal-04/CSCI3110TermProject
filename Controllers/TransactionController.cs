using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CSCI3110_Term_Project.Data;
using CSCI3110_Term_Project.Models;

namespace CSCI3110_Term_Project.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Transaction
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var list = _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == userId.Value)
                .OrderByDescending(t => t.Date)
                .ToList();

            return View(list);
        }

        // GET: /Transaction/Details/5
        public IActionResult Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var tx = _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefault(t => t.TransactionId == id && t.UserId == userId.Value);

            if (tx == null)
                return NotFound();

            return View(tx);
        }

        // GET: /Transaction/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: /Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transaction t)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(t);
            }

            t.UserId = userId.Value;
            _context.Transactions.Add(t);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Transaction/Edit/5
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var tx = _context.Transactions.Find(id);
            if (tx == null || tx.UserId != userId.Value)
                return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(tx);
        }

        // POST: /Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Transaction formTx)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(formTx);
            }

            var tx = _context.Transactions.Find(id);
            if (tx == null || tx.UserId != userId.Value)
                return NotFound();

            // update only allowed fields
            tx.Date = formTx.Date;
            tx.Type = formTx.Type;
            tx.Amount = formTx.Amount;
            tx.CategoryId = formTx.CategoryId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Transaction/Delete/5
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var tx = _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefault(t => t.TransactionId == id && t.UserId == userId.Value);

            if (tx == null)
                return NotFound();

            return View(tx);
        }

        // POST: /Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var tx = _context.Transactions.Find(id);
            if (tx == null || tx.UserId != userId.Value)
                return NotFound();

            _context.Transactions.Remove(tx);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
