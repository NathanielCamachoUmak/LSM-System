using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSM_prototype.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace LSM_prototype
{
    public class BenjaminDbContext : DbContext
    {
        public DbSet<Accounts> Accounts { get; set; } // Represents the Accounts table
        public DbSet<Item> Item { get; set; } // Represents the Item table
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Get the connection string from the App.config file
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Use the connection string for SQL Server
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
