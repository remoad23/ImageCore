using System.Collections.Generic;
using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class RoleSeeder : ISeeder
    {
        public static Dictionary<string,IdentityRole> Seed(ModelBuilder modelBuilder)
        {
            IdentityRole userRole = new IdentityRole("User");
            IdentityRole adminRole = new IdentityRole("Admin");
            IdentityRole projectViewerRole = new IdentityRole("ProjectViewer");
            IdentityRole projectEditorRole = new IdentityRole("ProjectEditor");
            IdentityRole projectOwnerRole = new IdentityRole("ProjectOwner");

            userRole.Id = "1";
            adminRole.Id = "2";
            projectViewerRole.Id = "3";
            projectEditorRole.Id = "4";
            projectOwnerRole.Id = "5";
            
            userRole.NormalizedName = "USER";
            adminRole.NormalizedName = "ADMIN";
            projectViewerRole.NormalizedName = "PROJECTVIEWER";
            projectEditorRole.NormalizedName = "PROJECTEDITOR";
            projectOwnerRole.NormalizedName = "PROJECTOWNER";
            
            
            modelBuilder.Entity<IdentityRole>().HasData(
                userRole,
                adminRole,
                projectViewerRole,
                projectEditorRole,
                projectOwnerRole
                );


            Dictionary<string, IdentityRole> roles = new Dictionary<string, IdentityRole>();
            
            roles.Add("User",userRole);
            roles.Add("Admin",adminRole);
            roles.Add("Viewer",projectViewerRole);
            roles.Add("Editor",projectEditorRole);
            roles.Add("Owner",projectOwnerRole);

            return roles;
            
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