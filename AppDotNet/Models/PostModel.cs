using AppDotNet.Entities;

namespace AppDotNet.Models
{
    public class PostModel : Post
    {
        public bool AlreadyLiked { get; set; }
        public bool BelongToMe { get; set; }

    }
}
