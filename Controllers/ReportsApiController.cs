using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSCI3110_Term_Project.Data;
using CSCI3110_Term_Project.Models;

namespace CSCI3110_Term_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReportsApiController(ApplicationDbContext context)
            => _context = context;

        // GET: /api/ReportsApi?userId=1&year=2025&month=5
        [HttpGet]
        public IActionResult GetMonthlyReport(
            [FromQuery] int userId,
            [FromQuery] int year,
            [FromQuery] int month)
        {
            // grab all transactions for that user/month
            var txs = _context.Transactions
                .Include(t => t.Category)
                .Where(t =>
                    t.UserId == userId
                    && t.Date.Year == year
                    && t.Date.Month == month)
                .ToList();

            // group by category name for expense sums
            var summaries = txs
                .GroupBy(t => t.Category.Name)
                .Select(g => new {
                    categoryName = g.Key,
                    totalSpent = Math.Round(g
                        .Where(t => t.Type == "Expense")
                        .Sum(t => t.Amount), 2)
                })
                .ToList();

            var totalIncome = Math.Round(txs
                .Where(t => t.Type == "Income")
                .Sum(t => t.Amount), 2);
            var totalExpense = Math.Round(txs
                .Where(t => t.Type == "Expense")
                .Sum(t => t.Amount), 2);

            return Ok(new
            {
                totalIncome,
                totalExpense,
                summaries
            });
        }
    }
}
