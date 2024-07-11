using FinalExam_991605014.Models;
using FinalExam_991605014.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalExam_991605014.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 1;
            var totalIncome = await _context.Incomes.Where(i => i.UserId == userId).SumAsync(i => i.Amount);
            var receivedIncomes = await _context.Incomes.Where(i => i.UserId == userId && i.IsReceived).ToListAsync();

            ViewBag.TotalIncome = totalIncome;
            ViewBag.ReceivedIncomes = receivedIncomes;

            return View();
        }
    }
}

