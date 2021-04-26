using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class RoleSeeder : ISeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            IdentityRole userRole = new IdentityRole("User");
            IdentityRole adminRole = new IdentityRole("Admin");
            IdentityRole projectViewerRole = new IdentityRole("ProjectViewer");
            IdentityRole projectEditorRole = new IdentityRole("ProjectEditor");
            IdentityRole projectOwnerRole = new IdentityRole("ProjectOwner");
            
            modelBuilder.Entity<IdentityRole>().HasData(
                userRole,
                adminRole,
                projectViewerRole,
                projectEditorRole,
                projectOwnerRole
                );
            
            // add roles to DB
            /*
            await roleManager.CreateAsync(new Role("User"));
            await roleManager.CreateAsync(new Role("Admin"));
            await roleManager.CreateAsync(new Role("ProjectViewer"));
            await roleManager.CreateAsync(new Role("ProjectEditor"));
            await roleManager.CreateAsync(new Role("ProjectOwner")); */
        }
    }
}