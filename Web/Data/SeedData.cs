using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DAL;
using Helper;
using Model;

namespace Web.Data
{
    public class SeedData
    {
        // Seed Admin
        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            using (var context = new BlogContext(serviceProvider.GetRequiredService<DbContextOptions<BlogContext>>()))
            {
                await context.Database.EnsureCreatedAsync();
                if (await context.Users.AnyAsync(u => u.UserName == Constants.AdminRole))
                {
                    return;
                }

                var adminID = await EnsureAdmin(serviceProvider);
                await EnsureRole(serviceProvider, adminID, Constants.AdminRole);
            }
        }

        private static async Task<int> EnsureAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            var user = await userManager.FindByNameAsync(Constants.AdminRole);
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Rafael",
                    LastName = "Bacus",
                    Email = "rafael.bacus@gmail.com",
                    UserName = Constants.AdminRole,
                    RowCreatedBy = 1,
                    RowCreatedDateTime = DateTime.Now,
                    RowModifiedBy = 1,
                    RowModifiedDateTime = DateTime.Now
                };

                await userManager.CreateAsync(user, Constants.DefaultPassword);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, int userId, string roleName)
        {
            IdentityResult result = null;
            var roleManager = serviceProvider.GetService<RoleManager<Role>>();

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new Role
                {
                    Name = roleName,
                    RowCreatedBy = 1,
                    RowCreatedDateTime = DateTime.Now,
                    RowModifiedBy = 1,
                    RowModifiedDateTime = DateTime.Now
                };

                result = await roleManager.CreateAsync(role);
            }

            var userManager = serviceProvider.GetService<UserManager<User>>();
            var user = await userManager.FindByIdAsync(userId.ToString());
            result = await userManager.AddToRoleAsync(user, roleName);
        
            return result;
        }

        // Seed Blog
        public static async Task SeedBlog(IServiceProvider serviceProvider)
        {
            using (var context = new BlogContext(serviceProvider.GetRequiredService<DbContextOptions<BlogContext>>()))
            {
                if (!await context.Blogs.AnyAsync())
                {
                    Blog blog = new Blog
                    {
                        Name = Constants.BlogName,
                        Url = Constants.BlogUrl,
                        RowCreatedBy = 1,
                        RowCreatedDateTime = DateTime.Now,
                        RowModifiedBy = 1,
                        RowModifiedDateTime = DateTime.Now
                    };

                    await context.Blogs.AddAsync(blog);
                    await context.SaveChangesAsync();
                }
            }

            return;
        }
    }
}