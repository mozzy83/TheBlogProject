using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TheBlogProject.Data;
using TheBlogProject.Enums;
using TheBlogProject.Models;

namespace TheBlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }



        public async Task ManageDataAsync()
        {
            //Task: Create the DB from the Migrations
            await _dbContext.Database.MigrateAsync();

            //Task 1: Seed a few Roles into the system
            await SeedRolesAsync();

            //Task 2: Seed a few users into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            //If there are already roles in the system, do nothing.
            if (_dbContext.Roles.Any()) return;

            //otherwise create a few roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                //Use the Role Manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            //If there are already users in the system, do nothing.
            if (_dbContext.Users.Any()) return;

            //Step 1: Creates a new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "userone@mailinator.com",
                UserName = "userone@mailinator.com",
                FirstName = "User1",
                LastName = "First",
                DisplayName = "The Admin",
                PhoneNumber = "(800) 555-1212",
                EmailConfirmed = true
            };
            //Step 2: Use the UserManager to create a new user that is defined by adminUser
            await _userManager.CreateAsync(adminUser, "Abc&123!");
            //Step 3: Add new user to Administrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());


            //Step 1 Repeat: Create the moderator user
            var modUser = new BlogUser()
            {
                Email = "usertwo@mailinator.com",
                UserName = "usertwo@mailinator.com",
                FirstName = "User2",
                LastName = "Second",
                DisplayName = "Moderator User",
                PhoneNumber = "(800) 555-1213",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(modUser, "Abc&123!");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());



        }


    }
}
