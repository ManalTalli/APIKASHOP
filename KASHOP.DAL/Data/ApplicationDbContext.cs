using System;
using System.Collections.Generic;
using System.Text;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace KASHOP.DAL.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Database=KASHOP;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");
        }
    }
}
