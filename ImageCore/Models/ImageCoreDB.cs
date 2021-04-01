using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Models
{
    public class ImageCoreDB : DbContext
    {
        private List<ContactModel> contacts;
        private List<ProjectModel> projects;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            ContactModel.Seed();
            ProjectModel.Seed();

        }
        
        
    }
}