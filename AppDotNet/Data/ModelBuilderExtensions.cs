using AppDotNet.Entities;
using AppDotNet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static i18n.Helpers.NuggetParser;

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
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            appUser.PasswordHash = "AQAAAAEAACcQAAAAENZzMPJwDZMu2HC3hfS5N3scdtb11VBrZIbrU0GglI5eT1dZChmRlcKxxSrYPl1rJQ==";

            //seed user
            modelBuilder.Entity<User>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "id_superviseur",
                UserId = "superviseur"
            });



            //un autre utisateur 
            var appUser2 = new User
            {
                Id = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                UserName = "john admin",
                NormalizedUserName = "JOHN_ADMIN"
            };

            //set user password
            PasswordHasher<User> ph2 = new PasswordHasher<User>();
            appUser2.PasswordHash = "AQAAAAEAACcQAAAAENZzMPJwDZMu2HC3hfS5N3scdtb11VBrZIbrU0GglI5eT1dZChmRlcKxxSrYPl1rJQ==";

            //seed user
            modelBuilder.Entity<User>().HasData(appUser2);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "id_admin_blog",
                UserId = "admin"
            });

            // add user 
            var userSimple = new User
            {
                Id = "user",
                Email = "user@gmail.com",
                EmailConfirmed = true,
                UserName = "john user",
                NormalizedUserName = "JOHN_USER"
            };

            //set user password
            PasswordHasher<User> ph3 = new PasswordHasher<User>();
            userSimple.PasswordHash = "AQAAAAEAACcQAAAAENZzMPJwDZMu2HC3hfS5N3scdtb11VBrZIbrU0GglI5eT1dZChmRlcKxxSrYPl1rJQ==";

            //seed user
            modelBuilder.Entity<User>().HasData(userSimple);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "id_utilisateur",
                UserId = "user"
            });


            /**
             * ADD BLOGS
             */
            // add user 
            //array of blogs
            string[] blogs = new string[21];
            blogs[0] = "";
            blogs[1] = "Ma vie en temps de crise";
            blogs[2] = "Les aventures de …";
            blogs[3] = "qualité produit";
            blogs[4] = "Je grandis avec vous";
            blogs[5] = "multimedia créatif";
            blogs[6] = "Temoignage d’experts";
            blogs[7] = "smartphone avis";
            blogs[8] = "techno simple";
            blogs[9] = "Parlez vous le blog ?";
            blogs[10] = "Ultra Blog";
            blogs[11] = "Influence toi";
            blogs[12] = "Mon microblog";
            blogs[13] = "Vlogy";
            blogs[14] = "Vlog expert";
            blogs[15] = "Perfection blog";
            blogs[16] = "Blog Towns";
            blogs[17] = "Blog Country";
            blogs[18] = "Blog It";
            blogs[19] = "WeBlog";
            blogs[20] = "NetBlog";

            for (int i = 1; i < 21; i++)
            {
                var prive = false;
                if(i%2 == 0)//si paire
                {
                    prive = true;
                }
             
                var blog = new Blog
                {
                    ID = i,
                    Name = blogs[i],
                    Prive = prive,
                    CreatedTimestamp = DateTime.Now,
                };
              
                //seed blog
                modelBuilder.Entity<Blog>().HasData(blog);
            }


           

        }
    }
}
