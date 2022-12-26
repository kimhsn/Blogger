namespace AppDotNet.Entities
{
    public class Post
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NbLikes { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = null!;
    }
}
