namespace TabloidMVC.Models.ViewModels
{
    public class PostCommentViewModel
    {
        public List<Comment> Comments { get; set; }

        public Comment Comment { get; set; }
        public Post Post { get; set; }
    }

}
