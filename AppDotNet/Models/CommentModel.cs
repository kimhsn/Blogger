using AppDotNet.Entities;

namespace AppDotNet.Models
{
    public class CommentModel:Comment
    {
        public string postName { get; set; }
        public string postDescription { get; set; }

        public int postId { get; set; }
    }
}
