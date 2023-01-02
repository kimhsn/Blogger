namespace AppDotNet.Entities
{
    public class Likes
    {
        public int ID { get; set; }
        public virtual User user { get; set; }

        public virtual Post post { get; set; }
    }
}
