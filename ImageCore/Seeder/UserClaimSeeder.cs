using System.Collections.Generic;
using System.Security.Claims;
using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class UserClaimSeeder : ISeeder
    {
        public static void Seed(ModelBuilder modelBuilder,List<ProjectModel> projects,List<UserModel> users)
        {
            // owners
            var userclaim = new IdentityUserClaim<string>();
            userclaim.Id = 1;
            userclaim.ClaimType = ClaimTypes.NameIdentifier;
            userclaim.ClaimValue = projects[0].ProjectId;
            userclaim.UserId = users[0].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim);
            
            // owners
            var userclaim2 = new IdentityUserClaim<string>();
            userclaim2.Id = 2;
            userclaim2.ClaimType = ClaimTypes.NameIdentifier;
            userclaim2.ClaimValue = projects[1].ProjectId;
            userclaim2.UserId = users[3].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim2);
            
            // owners
            var userclaim3 = new IdentityUserClaim<string>();
            userclaim3.Id = 3;
            userclaim3.ClaimType = ClaimTypes.NameIdentifier;
            userclaim3.ClaimValue = projects[2].ProjectId;
            userclaim3.UserId = users[0].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim3);
            
            // owners
            var userclaim4 = new IdentityUserClaim<string>();
            userclaim4.Id = 4;
            userclaim4.ClaimType = ClaimTypes.NameIdentifier;
            userclaim4.ClaimValue = projects[3].ProjectId;
            userclaim4.UserId = users[1].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim4);
            
            // owners
            var userclaim5 = new IdentityUserClaim<string>();
            userclaim5.Id = 5;
            userclaim5.ClaimType = ClaimTypes.NameIdentifier;
            userclaim5.ClaimValue = projects[4].ProjectId;
            userclaim5.UserId = users[2].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim5);
            
            // participator from here on ------------------------------------------

            var userclaim6 = new IdentityUserClaim<string>();
            userclaim6.Id = 6;
            userclaim6.ClaimType = ClaimTypes.NameIdentifier;
            userclaim6.ClaimValue = projects[0].ProjectId;
            userclaim6.UserId = users[1].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim6);
            
            var userclaim7 = new IdentityUserClaim<string>();
            userclaim7.Id = 7;
            userclaim7.ClaimType = ClaimTypes.NameIdentifier;
            userclaim7.ClaimValue = projects[0].ProjectId;
            userclaim7.UserId = users[2].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim7);
            
            var userclaim8 = new IdentityUserClaim<string>();
            userclaim8.Id = 8;
            userclaim8.ClaimType = ClaimTypes.NameIdentifier;
            userclaim8.ClaimValue = projects[1].ProjectId;
            userclaim8.UserId = users[1].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim8);
            
            var userclaim9 = new IdentityUserClaim<string>();
            userclaim9.Id = 9;
            userclaim9.ClaimType = ClaimTypes.NameIdentifier;
            userclaim9.ClaimValue = projects[3].ProjectId;
            userclaim9.UserId = users[0].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim9);
            
            var userclaim10 = new IdentityUserClaim<string>();
            userclaim10.Id = 10;
            userclaim10.ClaimType = ClaimTypes.NameIdentifier;
            userclaim10.ClaimValue = projects[3].ProjectId;
            userclaim10.UserId = users[2].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim10);
            
            var userclaim11 = new IdentityUserClaim<string>();
            userclaim11.Id = 11;
            userclaim11.ClaimType = ClaimTypes.NameIdentifier;
            userclaim11.ClaimValue = projects[3].ProjectId;
            userclaim11.UserId = users[3].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim11);
            
            var userclaim12 = new IdentityUserClaim<string>();
            userclaim12.Id = 12;
            userclaim12.ClaimType = ClaimTypes.NameIdentifier;
            userclaim12.ClaimValue = projects[4].ProjectId;
            userclaim12.UserId = users[0].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim12);
            
            var userclaim13 = new IdentityUserClaim<string>();
            userclaim13.Id = 13;
            userclaim13.ClaimType = ClaimTypes.NameIdentifier;
            userclaim13.ClaimValue = projects[4].ProjectId;
            userclaim13.UserId = users[1].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim13);
            
            var userclaim14 = new IdentityUserClaim<string>();
            userclaim14.Id = 14;
            userclaim14.ClaimType = ClaimTypes.NameIdentifier;
            userclaim14.ClaimValue = projects[4].ProjectId;
            userclaim14.UserId = users[3].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim14);
            
            var userclaim15 = new IdentityUserClaim<string>();
            userclaim15.Id = 15;
            userclaim15.ClaimType = ClaimTypes.NameIdentifier;
            userclaim15.ClaimValue = projects[4].ProjectId;
            userclaim15.UserId = users[4].Id;
            
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userclaim15);
        }
    }
}