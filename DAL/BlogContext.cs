using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class BlogContext : IdentityDbContext<User, Role, int>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<SysException> SysException { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new BlogConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new SysExceptionConfiguration());

            // ASPNET Core Identity Tables
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins")
                                                       .HasKey(userLogin => new
                                                       {
                                                           userLogin.LoginProvider,
                                                           userLogin.ProviderKey
                                                       });
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles")
                                                      .HasKey(userRole => new
                                                      {
                                                          userRole.UserId,
                                                          userRole.RoleId
                                                      });
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens")
                                                       .HasKey(userToken => new
                                                       {
                                                           userToken.UserId,
                                                           userToken.LoginProvider,
                                                           userToken.Name
                                                       });

        }
    }
}