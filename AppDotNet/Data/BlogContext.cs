using Microsoft.EntityFrameworkCore;
using AppDotNet.Entities;

namespace AppDotNet.Data
{
    public class BlogContext : DbContext
    {

        /*public BlogContext() : base("BlogContext")
        {
        }*/

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}