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
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
    }
}
