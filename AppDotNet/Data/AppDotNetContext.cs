using AppDotNet.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppDotNet.Data;

public class AppDotNetContext : IdentityDbContext<User>
{
    public AppDotNetContext(DbContextOptions<AppDotNetContext> options)
        : base(options)

    {
    }

	public DbSet<Blog> Blogs { get; set; }
	public DbSet<Post> Posts { get; set; }
	public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Likes> Likes { get; set; }





    //public string DbPath { get; }
    /*public BlogContext()
	{
		var folder = Environment.SpecialFolder.LocalApplicationData;
		var path = Environment.GetFolderPath(folder);
		DbPath = System.IO.Path.Join(path, "AppDotNet.db");
	}*/
    // The following configures EF to create a Sqlite database file in the // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		var configuration = new ConfigurationBuilder()
		   .SetBasePath(Directory.GetCurrentDirectory())
		   .AddJsonFile("appsettings.json")
		   .Build();

		options.UseSqlServer(configuration.GetConnectionString("AppDotNetContextConnection"));
	}

	protected override void OnModelCreating(ModelBuilder builder)
    {

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        base.OnModelCreating(builder);
        builder.Seed();
	


	}

}
