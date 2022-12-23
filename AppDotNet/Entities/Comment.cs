namespace AppDotNet.Entities
{
    public class Comment
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public virtual Post Post { get; set; }
    }
}
