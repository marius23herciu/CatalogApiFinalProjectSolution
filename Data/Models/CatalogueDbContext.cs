using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.Models
{
    public class CatalogueDbContext : DbContext
    {
        private readonly string connectionString;
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


        public CatalogueDbContext(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("LocalDb");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
        }

    }
}
