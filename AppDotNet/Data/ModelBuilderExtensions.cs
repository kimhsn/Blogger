using AppDotNet.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppDotNet.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "superviseur",
                NormalizedName = "SUPERVISEUR",
                Id = "id_superviseur",
                ConcurrencyStamp = "id_superviseur"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin_blog",
                NormalizedName = "ADMIN_BLOG",
                Id = "id_admin_blog",
                ConcurrencyStamp = "id_admin_blog"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "utilisateur",
                NormalizedName = "UTILISATEUR",
                Id = "id_utilisateur",
                ConcurrencyStamp = "id_utilisateur"
            });

            //create user
            var appUser = new User
            {
                Id = "superviseur",
                Email = "superviseur@gmail.com",
                EmailConfirmed = true,
                UserName = "john superviseur",
                NormalizedUserName = "JOHN_SUPERVISEUR"
            };

            //set user password
            PasswordHasher<User> ph = new PasswordHasher<User>();
            appUser.PasswordHash = ph.HashPassword(appUser, "123456mM@");

            //seed user
            modelBuilder.Entity<User>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "id_superviseur",
                UserId = "superviseur"
            });
        }
    }
}
