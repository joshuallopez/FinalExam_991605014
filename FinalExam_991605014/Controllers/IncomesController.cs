using FinalExam_991605014.Models;
using FinalExam_991605014.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_991605014.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IncomesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTotalIncome(int userId)
        {
            var totalIncome = await _context.Incomes.Where(i => i.UserId == userId).SumAsync(i => i.Amount);

            if (totalIncome == 0)
                return NotFound("No incomes found for the user.");

            return Ok(totalIncome);
        }

        // GET api
        [HttpGet("{userId}/{isReceived}")]
        public async Task<IActionResult> GetIncomesByReceipt(int userId, bool isReceived)
        {
            var incomes = await _context.Incomes.Where(i => i.UserId == userId && i.IsReceived == isReceived).ToListAsync();

            if (incomes == null || !incomes.Any())
                return NotFound("No incomes found matching the criteria.");

            return Ok(incomes);
        }

        // DELETE api
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
                return NotFound("Income not found.");

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();

            var totalIncome = await _context.Incomes.SumAsync(i => i.Amount);
            return Ok(totalIncome);
        }

        // POST api
        [HttpPost]
        public async Task<IActionResult> PostIncome([FromBody] Income income)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTotalIncome), new { userId = income.UserId }, income);
        }

        // PUT api
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] Income updatedIncome)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
                return NotFound("Income not found.");

            income.Description = updatedIncome.Description;
            income.Amount = updatedIncome.Amount;
            income.IsReceived = updatedIncome.IsReceived;
            income.UserId = updatedIncome.UserId;

            _context.Entry(income).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(income);
        }
    }
}

