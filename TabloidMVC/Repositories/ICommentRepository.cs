using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        void EditComment(Comment comment);
        Comment GetCommentById(int id);

    }
}
