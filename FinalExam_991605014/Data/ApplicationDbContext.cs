using Microsoft.EntityFrameworkCore;
using FinalExam_991605014.Models;

namespace FinalExam_991605014.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:TestDb");
            }
        }
    }
}

