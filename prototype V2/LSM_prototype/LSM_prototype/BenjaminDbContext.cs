using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSM_prototype.MVVM.Model;
using Microsoft.EntityFrameworkCore;

namespace LSM_prototype
{
    public class BenjaminDbContext : DbContext
    {
        public DbSet<Accounts> Accounts { get; set; } // Represents the Accounts table
        public DbSet<Item> Item { get; set; } // Represents the Item table
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OAKRFHN;Database=BenjaminDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
