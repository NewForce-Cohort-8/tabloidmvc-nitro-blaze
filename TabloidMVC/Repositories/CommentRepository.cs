<<<<<<< HEAD
﻿namespace TabloidMVC.Repositories
=======
﻿using TabloidMVC.Models;

namespace TabloidMVC.Repositories
>>>>>>> main
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
<<<<<<< HEAD
    }

}
=======

        public void Add(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Comment (
                            PostId, UserProfileId, Subject, Content, CreateDateTime )
                        OUTPUT INSERTED.ID
                        VALUES (
                            @PostId, @UserProfileId, @Subject, @Content, @CreateDateTime)";
                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);
                    cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


    }
}
>>>>>>> main
