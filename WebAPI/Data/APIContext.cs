using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Data
{
    public class APIContext: IdentityDbContext<User,Role,Guid>
    {
        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {

        }

        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<Role>().ToTable("Role");

            // any guid
            var roleAdminId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");

            var roleUserId = new Guid("9D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var userId = new Guid("59BD714F-9576-45BA-B5B7-F00649BE00DE");

            modelBuilder.Entity<Role>().HasData(new List<Role>
            {   new Role
                {
                    Id = roleAdminId,
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Administrator role"
                },
                new Role
                {
                    Id = roleUserId,
                    Name = "user",
                    NormalizedName = "user",
                    Description = "User role"
                },


            });

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new List<User>
            { new User
                {
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "tedu.international@gmail.com",
                    NormalizedEmail = "tedu.international@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "23042016Np@"),
                    SecurityStamp = string.Empty
                },
                new User
                {
                     Id = userId,
                    UserName = "user",
                    NormalizedUserName = "user",
                    Email = "tedu.international@gmail.com",
                    NormalizedEmail = "tedu.international@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "23042016Np@"),
                    SecurityStamp = string.Empty
                }

            });

            /* modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new List<IdentityUserRole<Guid>>
             { new IdentityUserRole<Guid>()
                 {
                     RoleId = roleAdminId,
                     UserId = adminId
                 },
                 new IdentityUserRole<Guid>()
                 {
                      RoleId = roleUserId,
                     UserId = userId
                 },

             });*/

            modelBuilder.Entity<News>().HasData(new List<News>
            { new News()
            {
                NewsId =1,
                Theme = "The Thao",
                Content = "aaaaaaaaaaaaaaaaaaaaaaa",
                Title = "Bong da",
                CreatedBy = "user"

            },
             new News()
            {
                NewsId =2,
                Theme = "The Thao",
                Content = "bbbbbbbbbbbbbbbbbbbbbb",
                Title = "Bong chuyen",
                CreatedBy ="user"

            },
             new News()
            {
                 NewsId =3,
                Theme = "The Thao",
                Content = "ccccccccccccccccccccccc",
                Title = "Bong ro",
                CreatedBy ="user"

            }
            }); 

        }

    }
}
