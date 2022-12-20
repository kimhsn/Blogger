using System.Collections.Generic;

using System;
using System.Diagnostics;

namespace AppDotNet.Entities
{
    public class Blog
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Prive { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public virtual User Admin { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = null!;
    }
}
