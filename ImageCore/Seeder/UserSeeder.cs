﻿using System.Threading.Tasks;
using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class UserSeeder : ISeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
           var hasher = new PasswordHasher<UserModel>();

           // init users
            var user1 = new UserModel
            {
               Email = "user1@gmail.com",
               UserName = "User1",
               NormalizedUserName = "User1",
               PasswordHash = hasher.HashPassword(null, "passworduser1")
            };
            
            var user2 = new UserModel
            {
               Email = "use2r@gmail.com",
               UserName = "User2",
               NormalizedUserName = "User2",
               PasswordHash = hasher.HashPassword(null, "passworduser2")
            };
            
            var user3 = new UserModel
            {
               Email = "user3@gmail.com",
               UserName = "User3",
               NormalizedUserName = "User3",
               PasswordHash = hasher.HashPassword(null, "passworduser3")
            };
            
            var user4 = new UserModel
            {
               Email = "user4@gmail.com",
               UserName = "User4",
               NormalizedUserName = "User4",
               PasswordHash = hasher.HashPassword(null, "passworduser4")
            };
            
            var user5 = new UserModel
            {
               Email = "user5@gmail.com",
               UserName = "User5",
               NormalizedUserName = "User5",
               PasswordHash = hasher.HashPassword(null, "passworduser5")
            };
            
            var user6 = new UserModel
            {
               Email = "user6@gmail.com",
               UserName = "User6",
               NormalizedUserName = "User6",
               PasswordHash = hasher.HashPassword(null, "passworduser6")
            };
            
            var user7 = new UserModel
            {
               Email = "user7@gmail.com",
               UserName = "User7",
               NormalizedUserName = "User7",
               PasswordHash = hasher.HashPassword(null, "passworduser7")
            };
            
            var user8 = new UserModel
            {
               Email = "user8@gmail.com",
               UserName = "User8",
               NormalizedUserName = "User8",
               PasswordHash = hasher.HashPassword(null, "passworduser8")
            };
            
            var user9 = new UserModel
            {
                Email = "user9@gmail.com",
                UserName = "User9",
                NormalizedUserName = "User9",
                PasswordHash = hasher.HashPassword(null, "passworduser9")
            };
            
            var user10 = new UserModel
            {
                Email = "user10@gmail.com",
                UserName = "User10",
                NormalizedUserName = "User10",
                PasswordHash = hasher.HashPassword(null, "passworduser10")
            };
            
            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<UserModel>().HasData(
                user1,
                user2,
                user3,
                user4,
                user5,
                user6,
                user7,
                user8,
                user9,
                user10
            );
            
        }
    }
}