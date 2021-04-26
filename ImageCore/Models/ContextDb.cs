using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImageCore.Models
{
    /**
     * Objectification of the database of the project
     */
    public class ContextDb : IdentityDbContext<UserModel>
    {
        
        public ContextDb(DbContextOptions<ContextDb> options): base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RenameDefaultTables(modelBuilder);
            Seeder.Seeder.SeedIdentityDb(modelBuilder);
        }

        /**
         * Rename the default tables of  Asp.NET Identity entities
         */
        private void RenameDefaultTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>  entity.ToTable(name: "Users") );
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>  entity.ToTable("UserRoles") );
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>  entity.ToTable("UserClaims") );
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>  entity.ToTable("RoleClaims") );
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>  entity.ToTable("UserLogins") );
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>  entity.ToTable("UserTokens") );
            modelBuilder.Entity<RoleModel>(entity =>  entity.ToTable(name: "Roles") );
        }
    }
}