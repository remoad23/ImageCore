using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class UserRoleSeeder : ISeeder
    {

        public static void Seed(ModelBuilder modelBuilder)
        {
            IdentityUserRole<int> User1Role = new IdentityUserRole<int>
            {
                UserId = 1,
                RoleId = 2,
            };
            IdentityUserRole<int> User2Role = new IdentityUserRole<int>
            {
                UserId = 2,
                RoleId = 2,
            };
            IdentityUserRole<int> User3Role = new IdentityUserRole<int>
            {
                UserId = 3,
                RoleId = 2,
            };
            IdentityUserRole<int> User4Role = new IdentityUserRole<int>
            {
                UserId = 4,
                RoleId = 1,
            };
            IdentityUserRole<int> User5Role = new IdentityUserRole<int>
            {
                UserId = 5,
                RoleId = 1,
            };
            IdentityUserRole<int> User6Role = new IdentityUserRole<int>
            {
                UserId = 6,
                RoleId = 1,
            };
            IdentityUserRole<int> User7Role = new IdentityUserRole<int>
            {
                UserId = 7,
                RoleId = 8,
            };
            IdentityUserRole<int> User8Role = new IdentityUserRole<int>
            {
                UserId = 9,
                RoleId = 1,
            };
            IdentityUserRole<int> User9Role = new IdentityUserRole<int>
            {
                UserId = 10,
                RoleId = 1,
            };

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                User1Role,
                User2Role,
                User3Role,
                User4Role,
                User5Role,
                User6Role,
                User7Role,
                User8Role,
                User9Role
            );
        }
    }
}