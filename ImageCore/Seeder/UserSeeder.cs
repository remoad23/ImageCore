using System.Collections.Generic;
using System.Threading.Tasks;
using ImageCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    public class UserSeeder : ISeeder
    {
        public static List<UserModel> Seed(ModelBuilder modelBuilder)
        {
           var hasher = new PasswordHasher<UserModel>();

           // init users
            var user1 = new UserModel
            {
               Email = "imagecore23@gmail.com",
               UserName = "User1",
               NormalizedUserName = "USER1",
               NormalizedEmail = "IMAGECORE23@GMAIL.COM",
               PasswordHash = hasher.HashPassword(null, "Password23@"),
               Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user2 = new UserModel
            {
               Email = "imagecore24@gmail.com",
               UserName = "User2",
               NormalizedUserName = "USER2",
               NormalizedEmail = "IMAGECORE24@GMAIL.COM",
               PasswordHash = hasher.HashPassword(null, "Password24@"),
               Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user3 = new UserModel
            {
               Email = "imagecoreuser@gmail.com",
               UserName = "User3",
               NormalizedUserName = "USER3",
               NormalizedEmail = "IMAGECOREUSER@GMAIL.COM",
               PasswordHash = hasher.HashPassword(null, "Passworduser@"),
               Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user4 = new UserModel
            {
               Email = "user4@gmail.com",
               UserName = "User4",
               NormalizedUserName = "USER4",
               NormalizedEmail = "USER4@GMAIL.COM",
               PasswordHash = hasher.HashPassword(null, "passworduser4"),
               Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user5 = new UserModel
            {
               Email = "user5@gmail.com",
               UserName = "User5",
               NormalizedEmail = "USER5@GMAIL.COM",
               NormalizedUserName = "USER5",
               PasswordHash = hasher.HashPassword(null, "passworduser5"),
               Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user6 = new UserModel
            {
               Email = "user6@gmail.com",
               UserName = "User6",
               NormalizedUserName = "USER6",
               NormalizedEmail = "USER6@GMAIL.COM",
               PasswordHash = hasher.HashPassword(null, "passworduser6"),
               Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user7 = new UserModel
            {
               Email = "user7@gmail.com",
               UserName = "User7",
               NormalizedUserName = "USER7",
               NormalizedEmail = "USER7@GMAIL.COM",
               PasswordHash = hasher.HashPassword(null, "passworduser7"),
               Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user8 = new UserModel
            {
               Email = "user8@gmail.com",
               UserName = "User8",
               NormalizedUserName = "USER8",
               NormalizedEmail = "USER8@GMAIL.COM",
               PasswordHash = hasher.HashPassword(null, "passworduser8"),
               Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user9 = new UserModel
            {
                Email = "user9@gmail.com",
                UserName = "User9",
                NormalizedUserName = "USER9",
                NormalizedEmail = "USER9@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "passworduser9"),
                Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
            };
            
            var user10 = new UserModel
            {
                Email = "user10@gmail.com",
                UserName = "User10",
                NormalizedUserName = "USER10",
                NormalizedEmail = "USER10@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "passworduser10"),
                Description = "Hallo! Ich bin eine Beschreibung über dieses Profil!",
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

            var users = new List<UserModel>();
            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(user4);
            users.Add(user5);
            users.Add(user6);
            users.Add(user7);
            users.Add(user8);
            users.Add(user9);
            users.Add(user10);
            
            
            for (int x = 11; x <= 20; x++)
            {
               var user = new UserModel
               {
                  Email = $"user{x}@gmail.com",
                  UserName = "User" + x,
                  NormalizedUserName = "USER" +x,
                  NormalizedEmail = $"USER{x}@GMAIL.COM",
                  PasswordHash = hasher.HashPassword(null, "passworduser" + x)
               };
               modelBuilder.Entity<UserModel>().HasData(user);
               users.Add(user);
            }
            
            return users;

        }
    }
}