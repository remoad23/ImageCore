using System.Collections.Generic;
using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class RoleSeeder : ISeeder
    {
        public static Dictionary<string,RoleModel> Seed(ModelBuilder modelBuilder)
        {
            RoleModel userRole = new RoleModel("User");
            RoleModel adminRole = new RoleModel("Admin");
            RoleModel projectViewerRole = new RoleModel("ProjectViewer");
            RoleModel projectEditorRole = new RoleModel("ProjectEditor");
            RoleModel projectOwnerRole = new RoleModel("ProjectOwner");
            
            modelBuilder.Entity<IdentityRole>().HasData(
                userRole,
                adminRole,
                projectViewerRole,
                projectEditorRole,
                projectOwnerRole
                );


            Dictionary<string, RoleModel> roles = new Dictionary<string, RoleModel>();
            
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