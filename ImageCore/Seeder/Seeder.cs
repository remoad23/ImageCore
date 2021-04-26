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
        public static void SeedIdentityDb(ModelBuilder? modelBuilder = null)
        {
            UserSeeder.Seed(modelBuilder);
            ProjectSeeder.Seed();
            RoleSeeder.Seed(modelBuilder);
            ContactSeeder.Seed();
        }

    }
}