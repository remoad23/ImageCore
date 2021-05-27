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
            var users = UserSeeder.Seed(modelBuilder);
            RoleSeeder.Seed(modelBuilder);
           // UserRoleSeeder.Seed(modelBuilder);
            ContactSeeder.Seed(modelBuilder,users);
            ProjectSeeder.Seed(modelBuilder,users);
          //  FilterSeeder.Seed();
        //    ImageComponentSeeder.Seed();
      //      ImageLayerSeeder.Seed();
          //  ProjectParticipatorSeeder.Seed(modelBuilder,users);
    //        FilterSeeder.Seed();
        }

    }
}