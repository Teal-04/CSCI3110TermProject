using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CSCI3110_Term_Project.Data;
using CSCI3110_Term_Project.Models;

namespace CSCI3110_Term_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        // GET: /Category
        public IActionResult Index()
        {
            var list = _context.Categories.ToList();
            return View(list);
        }

        // GET: /Category/Create
        public IActionResult Create() => View();

        // POST: /Category/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            if (!ModelState.IsValid) return View(cat);

            _context.Categories.Add(cat);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Category/Edit/5
        public IActionResult Edit(int id)
        {
            var cat = _context.Categories.Find(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        // POST: /Category/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category cat)
        {
            if (id != cat.CategoryId) return BadRequest();
            if (!ModelState.IsValid) return View(cat);

            _context.Categories.Update(cat);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Category/Delete/5
        public IActionResult Delete(int id)
        {
            var cat = _context.Categories.Find(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cat = _context.Categories.Find(id);
            if (cat == null) return NotFound();
            _context.Categories.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Category/Details/5
        public IActionResult Details(int id)
        {
            var cat = _context.Categories.Find(id);
            if (cat == null) return NotFound();
            return View(cat);
        }
    }
}
