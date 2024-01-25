using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(int id);
        Comment GetCommentById(int id);

    }
}
