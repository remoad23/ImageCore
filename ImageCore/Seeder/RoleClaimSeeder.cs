using System.Collections.Generic;
using System.Security.Claims;
using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class RoleClaimSeeder : ISeeder
    {
        public static void Seed(ModelBuilder modelBuilder,List<ProjectModel> projects,List<UserModel> users,Dictionary<string,IdentityRole> roles)
        {
            // owners
            var roleclaim = new RoleClaim();
            roleclaim.Id = 1;
            roleclaim.ClaimType = ClaimTypes.Role;
            roleclaim.ClaimValue = users[0].Id;
            roleclaim.ClaimValue2 = projects[0].ProjectId;
            roleclaim.RoleId = roles["Owner"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim); 
            
            var roleclaim2 = new RoleClaim();
            roleclaim2.Id = 2;
            roleclaim2.ClaimType = ClaimTypes.Role;
            roleclaim2.ClaimValue = users[3].Id;
            roleclaim2.ClaimValue2 = projects[1].ProjectId;
            roleclaim2.RoleId = roles["Owner"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim2); 
            
            var roleclaim3 = new RoleClaim();
            roleclaim3.Id = 3;
            roleclaim3.ClaimType = ClaimTypes.Role;
            roleclaim3.ClaimValue = users[0].Id;
            roleclaim3.ClaimValue2 = projects[2].ProjectId;
            roleclaim3.RoleId = roles["Owner"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim3); 
            
            var roleclaim4 = new RoleClaim();
            roleclaim4.Id = 4;
            roleclaim4.ClaimType = ClaimTypes.Role;
            roleclaim4.ClaimValue = users[1].Id;
            roleclaim4.ClaimValue2 = projects[3].ProjectId;
            roleclaim4.RoleId = roles["Owner"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim4); 
            
            var roleclaim5 = new RoleClaim();
            roleclaim5.Id = 5;
            roleclaim5.ClaimType = ClaimTypes.Role;
            roleclaim5.ClaimValue = users[2].Id;
            roleclaim5.ClaimValue2 = projects[4].ProjectId;
            roleclaim5.RoleId = roles["Owner"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim5); 
            
            // participator from here on ------------------------------
            
            var roleclaim6 = new RoleClaim();
            roleclaim6.Id = 6;
            roleclaim6.ClaimType = ClaimTypes.Role;
            roleclaim6.ClaimValue = users[1].Id;
            roleclaim6.ClaimValue2 = projects[0].ProjectId;
            roleclaim6.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim6); 
            
            var roleclaim7 = new RoleClaim();
            roleclaim7.Id = 7;
            roleclaim7.ClaimType = ClaimTypes.Role;
            roleclaim7.ClaimValue = users[2].Id;
            roleclaim7.ClaimValue2 = projects[0].ProjectId;
            roleclaim7.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim7); 
            
            var roleclaim8 = new RoleClaim();
            roleclaim8.Id = 8;
            roleclaim8.ClaimType = ClaimTypes.Role;
            roleclaim8.ClaimValue = users[1].Id;
            roleclaim8.ClaimValue2 = projects[1].ProjectId;
            roleclaim8.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim8); 
            
            var roleclaim9 = new RoleClaim();
            roleclaim9.Id = 9;
            roleclaim9.ClaimType = ClaimTypes.Role;
            roleclaim9.ClaimValue = users[0].Id;
            roleclaim9.ClaimValue2 = projects[3].ProjectId;
            roleclaim9.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim9); 
            
            var roleclaim10 = new RoleClaim();
            roleclaim10.Id = 10;
            roleclaim10.ClaimType = ClaimTypes.Role;
            roleclaim10.ClaimValue = users[2].Id;
            roleclaim10.ClaimValue2 = projects[3].ProjectId;
            roleclaim10.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim10); 
            
            var roleclaim11 = new RoleClaim();
            roleclaim11.Id = 11;
            roleclaim11.ClaimType = ClaimTypes.Role;
            roleclaim11.ClaimValue = users[3].Id;
            roleclaim11.ClaimValue2 = projects[3].ProjectId;
            roleclaim11.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim11); 
            
            var roleclaim12 = new RoleClaim();
            roleclaim12.Id = 12;
            roleclaim12.ClaimType = ClaimTypes.Role;
            roleclaim12.ClaimValue = users[0].Id;
            roleclaim12.ClaimValue2 = projects[4].ProjectId;
            roleclaim12.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim12); 
            
            var roleclaim13 = new RoleClaim();
            roleclaim13.Id = 13;
            roleclaim13.ClaimType = ClaimTypes.Role;
            roleclaim13.ClaimValue = users[1].Id;
            roleclaim13.ClaimValue2 = projects[4].ProjectId;
            roleclaim13.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim13); 
            
            var roleclaim14 = new RoleClaim();
            roleclaim14.Id = 14;
            roleclaim14.ClaimType = ClaimTypes.Role;
            roleclaim14.ClaimValue = users[3].Id;
            roleclaim14.ClaimValue2 = projects[4].ProjectId;
            roleclaim14.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim14); 
            
            var roleclaim15 = new RoleClaim();
            roleclaim15.Id = 15;
            roleclaim15.ClaimType = ClaimTypes.Role;
            roleclaim15.ClaimValue = users[4].Id;
            roleclaim15.ClaimValue2 = projects[4].ProjectId;
            roleclaim15.RoleId = roles["Editor"].Id;
            
            modelBuilder.Entity<RoleClaim>().HasData(roleclaim15); 
        }
    }
}