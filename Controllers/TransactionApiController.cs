using Microsoft.AspNetCore.Mvc;
using CSCI3110_Term_Project.Data;
using CSCI3110_Term_Project.Models;

namespace CSCI3110_Term_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        public TransactionsApiController(ApplicationDbContext ctx) => _ctx = ctx;

        // GET: api/TransactionsApi
        [HttpGet]
        public IActionResult GetAll() =>
          Ok(_ctx.Transactions.ToList());

        // POST: api/TransactionsApi
        [HttpPost]
        public IActionResult Create([FromBody] Transaction tx)
        {
            _ctx.Transactions.Add(tx);
            _ctx.SaveChanges();
            return CreatedAtAction(nameof(GetAll), new { id = tx.TransactionId }, tx);
        }

        // PUT: api/TransactionsApi/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Transaction tx)
        {
            if (id != tx.TransactionId) return BadRequest();
            _ctx.Transactions.Update(tx);
            _ctx.SaveChanges();
            return NoContent();
        }

        // DELETE: api/TransactionsApi/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tx = _ctx.Transactions.Find(id);
            if (tx == null) return NotFound();
            _ctx.Transactions.Remove(tx);
            _ctx.SaveChanges();
            return NoContent();
        }
    }
}
