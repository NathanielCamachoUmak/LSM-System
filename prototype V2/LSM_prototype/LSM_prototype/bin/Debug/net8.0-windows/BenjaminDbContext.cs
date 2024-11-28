using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSM_prototype.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LSM_prototype
{
    public class BenjaminDbContext : DbContext
    {
        public DbSet<Accounts>? Accounts { get; set; } // Represents the Accounts table
        public DbSet<Item>? Item { get; set; } // Represents the Item table
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            
        }
    }
}
