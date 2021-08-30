using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace ImageCore.Models
{
    /**
     * Objectification of the database of the project
     */
    public class ContextDb : IdentityDbContext<
        UserModel, IdentityRole, string,
        IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>,
        RoleClaim, IdentityUserToken<string>>
    {
        public DbSet<FilterModel> Filters { get; set; }
        public DbSet<ImageComponentModel> ImageComponent { get; set; }
        public DbSet<ImageLayerModel> ImageLayer { get; set; }
        public DbSet<ProjectModel> Project { get; set; }
        public DbSet<ProjectParticipatorModel> ProjectParticipator { get; set; }
        public DbSet<ContactModel> Contact { get; set; }
        
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
            Seeder.Seeder.SeedDb(modelBuilder);

        }

        /**
         * Rename the default tables of  Asp.NET Identity entities
         */
        private void RenameDefaultTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>  entity.ToTable(name: "Users") );
            modelBuilder.Entity<IdentityRole>(entity =>  entity.ToTable(name: "Roles") );
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>  entity.ToTable("UserRoles") );
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>  entity.ToTable("UserClaims") );
            modelBuilder.Entity<RoleClaim>(entity =>  entity.ToTable("RoleClaims") );
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>  entity.ToTable("UserLogins") );
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>  entity.ToTable("UserTokens") );
        }
    }
}