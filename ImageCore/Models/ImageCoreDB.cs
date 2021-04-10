using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Models
{
    /**
     * Objectification of the database of the project
     */
    public class ImageCoreDB : IdentityDbContext<UserModel>
    {
        public DbSet<ContactModel> Contacts  { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }

        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        
        public ImageCoreDB(
            DbContextOptions<ImageCoreDB> options,
            RoleManager<IdentityRole> _roleManager,
            UserManager<IdentityUser> _userManager) : base(options)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RenameDefaultTables(modelBuilder);
            await InitRoles();
            await InitClaims();
            await InitIdentityUsers();
            
            ContactModel.Seed();
            ProjectModel.Seed();

        }

        /**
         * Make Roles for the Roles inside roles Table
         */
        private async Task InitRoles()
        {
            // add roles to DB
            await roleManager.CreateAsync(new IdentityRole("User"));
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("ProjectViewer"));
            await roleManager.CreateAsync(new IdentityRole("ProjectEditor"));
            await roleManager.CreateAsync(new IdentityRole("ProjectOwner"));
        }

        private async Task InitClaims()
        {
            
        }
        
        private async Task InitIdentityUsers()
        {
            // init users
            var user1 = new IdentityUser
            {
                Email = "user1@gmail.com",
                UserName = "User1",
            };
            
            var user2 = new IdentityUser
            {
                Email = "use2r@gmail.com",
                UserName = "User2",
            };
            
            var user3 = new IdentityUser
            {
                Email = "user3@gmail.com",
                UserName = "User3",
            };
            
            var user4 = new IdentityUser
            {
                Email = "user4@gmail.com",
                UserName = "User4",
            };
            
            var user5 = new IdentityUser
            {
                Email = "user5@gmail.com",
                UserName = "User5",
            };
            
            var user6 = new IdentityUser
            {
                Email = "user6@gmail.com",
                UserName = "User6",
            };
            
            var user7 = new IdentityUser
            {
                Email = "user7@gmail.com",
                UserName = "User7",
            };
            
            var user8 = new IdentityUser
            {
                Email = "user8@gmail.com",
                UserName = "User8",
            };
            
            var user9 = new IdentityUser
            {
                Email = "user9@gmail.com",
                UserName = "User9",
            };
            
            var user10 = new IdentityUser
            {
                Email = "user10@gmail.com",
                UserName = "User10",
            };


            // add users to DB
            await userManager.CreateAsync(user1);
            await userManager.CreateAsync(user2);
            await userManager.CreateAsync(user3);
            await userManager.CreateAsync(user4);
            await userManager.CreateAsync(user5);
            await userManager.CreateAsync(user6);
            await userManager.CreateAsync(user7);
            await userManager.CreateAsync(user8);
            await userManager.CreateAsync(user9);
            await userManager.CreateAsync(user10);
        }

        /**
         * Rename the Default tables of  Asp.NET Identity
         */
        private void RenameDefaultTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>  entity.ToTable(name: "Users") );
            modelBuilder.Entity<IdentityRole>(entity =>  entity.ToTable(name: "Roles") );
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>  entity.ToTable("UserRoles") );
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>  entity.ToTable("UserClaims") );
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>  entity.ToTable("RoleClaims") );
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>  entity.ToTable("UserLogins") );
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>  entity.ToTable("UserTokens") );
        }
    }
}