using System;
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
            IdentityRole projectEditorRole = new IdentityRole("ProjectEditor");
            IdentityRole projectOwnerRole = new IdentityRole("ProjectOwner");

            userRole.Id = Guid.NewGuid().ToString();
            adminRole.Id = Guid.NewGuid().ToString();
            projectEditorRole.Id = Guid.NewGuid().ToString();
            projectOwnerRole.Id = Guid.NewGuid().ToString();
            
            userRole.NormalizedName = "USER";
            adminRole.NormalizedName = "ADMIN";
            projectEditorRole.NormalizedName = "PROJECTEDITOR";
            projectOwnerRole.NormalizedName = "PROJECTOWNER";
            
            
            modelBuilder.Entity<IdentityRole>().HasData(
                userRole,
                adminRole,
                projectEditorRole,
                projectOwnerRole
                );


            Dictionary<string, IdentityRole> roles = new Dictionary<string, IdentityRole>();
            
            roles.Add("User",userRole);
            roles.Add("Admin",adminRole);
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