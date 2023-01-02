using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AppDotNet.Entities;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
	public virtual ICollection<Blog> Blogs { get; set; } = null!;

    public virtual ICollection<Likes> Likes { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = null!;
}

