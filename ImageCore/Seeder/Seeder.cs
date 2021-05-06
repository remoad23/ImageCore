using System;
using System.Linq;
using ImageCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    /**
     * Main Seeder to seed all the Seeder in here
     */
    public static class Seeder
    {
        #nullable enable
        public static void SeedDb(ModelBuilder? modelBuilder = null)
        {
            UserSeeder.Seed(modelBuilder);
            RoleSeeder.Seed(modelBuilder);
           // UserRoleSeeder.Seed(modelBuilder);
            ContactSeeder.Seed();
            ProjectSeeder.Seed();
        }

    }
}