using System;
using System.Collections.Generic;
using System.Linq;
using ImageCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Seeder
{
    /**
     * Main Seeder to seed all the Seeder in here
     * All subseeder can have a relationship and factory class
     */
    public static class Seeder
    {
        
        #nullable enable
        public static void SeedDb(ModelBuilder? modelBuilder = null)
        {
            //Seeder
            UserSeeder.Seed(modelBuilder);
            RoleSeeder.Seed(modelBuilder);
           // UserRoleSeeder.Seed(modelBuilder);
            ContactSeeder.Seed();
            ProjectSeeder.Seed();
            FilterSeeder.Seed();
            ImageComponentSeeder.Seed();
            ImageLayerSeeder.Seed();
            ProjectParticipatorSeeder.Seed();
            FilterSeeder.Seed();
        }

    }
}