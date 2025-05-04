using Microsoft.AspNetCore.Mvc;
using CSCI3110_Term_Project.Data;
using CSCI3110_Term_Project.Models;
using System.Linq;

namespace CSCI3110_Term_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BudgetApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/BudgetApi
        [HttpGet]
        public IActionResult GetAll()
        {
            var budgets = _context.Budgets.ToList();
            return Ok(budgets);
        }

        // GET: /api/BudgetApi/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var budget = _context.Budgets.Find(id);
            if (budget == null) return NotFound();
            return Ok(budget);
        }

        // POST: /api/BudgetApi
        [HttpPost]
        public IActionResult Create([FromBody] Budget budget)
        {
            _context.Budgets.Add(budget);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = budget.BudgetId }, budget);
        }

        // PUT: /api/BudgetApi/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Budget budget)
        {
            if (id != budget.BudgetId) return BadRequest();
            _context.Budgets.Update(budget);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: /api/BudgetApi/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var budget = _context.Budgets.Find(id);
            if (budget == null) return NotFound();
            _context.Budgets.Remove(budget);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
