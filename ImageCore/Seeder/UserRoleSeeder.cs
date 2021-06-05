using System.Collections.Generic;
using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class UserRoleSeeder : ISeeder
    {

        public static void Seed(ModelBuilder modelBuilder,List<UserModel> userModels,Dictionary<string,RoleModel> roles)
        {
            for (int x = 0; x < 4; x++)
            {
                IdentityUserRole<string> UserRole = new IdentityUserRole<string>
                {
                    UserId = userModels[0].Id,
                    RoleId = roles["Admin"].Id,
                };
                
                modelBuilder.Entity<IdentityUserRole<string>>().HasData(UserRole);
            }

            for (int x = 5; x < 20; x++)
            {
                IdentityUserRole<string> UserRole = new IdentityUserRole<string>
                {
                    UserId = userModels[0].Id,
                    RoleId = roles["User"].Id
                };
                
                modelBuilder.Entity<IdentityUserRole<string>>().HasData(UserRole);
            }



            
        }
    }
}