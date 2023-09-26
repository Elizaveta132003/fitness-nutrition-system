﻿using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.DataContext
{
    /// <summary>
    /// Seed class for initializing the application database with default data.
    /// </summary>
    /// </summary>
    public class ApplicationContextSeed
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        /// <summary>
        /// Initializes a new instance of the ApplicationContextSeed class.
        /// </summary>
        /// <param name="userManager">The user manager for managing user data.</param>
        /// <param name="roleManager">The role manager for managing roles.</param>
        public ApplicationContextSeed(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Seeds the database with initial data if it's empty.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {
                await SeedUsers();
            }

            if (!_roleManager.Roles.Any())
            {
                await SeedRoles();
            }

            if (!_userManager.GetUsersInRoleAsync(Role.admin.ToString()).Result.Any())
            {
                await SeedUserRoles();
            }
        }

        /// <summary>
        /// Seeds the database with initial user data.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task SeedUsers()
        {
            var superAdminUser = new AppUser()
            {
                UserName = "superadmin",
                NormalizedUserName = "SUPERADMIN",
                Gender = Gender.Female
            };

            await _userManager.CreateAsync(superAdminUser, "SuperAdmin123$");
        }

        /// <summary>
        /// Seeds the database with initial role data.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task SeedRoles()
        {
            var adminRole = new IdentityRole<Guid>()
            {
                Name = Role.admin.ToString()
            };

            var userRole = new IdentityRole<Guid>()
            {
                Name = Role.user.ToString()
            };

            await _roleManager.CreateAsync(adminRole);
            await _roleManager.CreateAsync(userRole);
        }

        /// <summary>
        /// Seeds the database with initial user-role associations.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task SeedUserRoles()
        {
            var superAdminUser = await _userManager.FindByNameAsync("superadmin");
            var adminRole = await _roleManager.FindByNameAsync(Role.admin.ToString());

            if (superAdminUser != null && adminRole != null)
            {
                await _userManager.AddToRoleAsync(superAdminUser, adminRole.Name);
            }
        }
    }
}