using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Models
{
    public class ImageCoreDB : DbContext
    {
        public DbSet<ContactModel> Contacts  { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        
        public ImageCoreDB(DbContextOptions<ImageCoreDB> options) : base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ContactModel.Seed();
            ProjectModel.Seed();

        }
        
        
    }
}