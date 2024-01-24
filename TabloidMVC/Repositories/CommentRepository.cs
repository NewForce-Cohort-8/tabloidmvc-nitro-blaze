using Microsoft.Data.SqlClient;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories

{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }

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

        public void EditComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         UPDATE Comment
                            SET 
                                PostId = @PostId,
                                UserProfileId = @UserProfileId,
                                Subject = @Subject,
                                Content = @Content,
                                CreateDateTime = @CreateDateTime
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@Id", comment.Id);
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);
                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Comment GetCommentById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT p.Id,
                              c.Id AS ""Comment Id"", c.PostId, c.UserProfileId, c.Subject, c.Content AS ""Comment Content"", c.CreateDateTime,
                              u.FirstName, u.LastName, u.DisplayName, 
                              u.Email, u.CreateDateTime, u.ImageLocation AS AvatarImage,
                              u.UserTypeId, 
                              ut.[Name] AS UserTypeName
                         FROM Comment c 
                              LEFT JOIN Post p ON c.PostId = p.id
                              LEFT JOIN UserProfile u ON p.UserProfileId = u.id
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE  c.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);


                    var reader = cmd.ExecuteReader();

                    Comment comment = null;

                    if (reader.Read())
                    {
                        comment = NewCommentOnPost(reader);
                    }

                    reader.Close();

                    return comment;
                }
            }
        }

        private Comment NewCommentOnPost(SqlDataReader reader)
        {
            return new Comment()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Comment Id")),
                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                Content = reader.GetString(reader.GetOrdinal("Comment Content")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                UserProfile = new UserProfile()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                    ImageLocation = DbUtils.GetNullableString(reader, "AvatarImage"),
                    UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                    UserType = new UserType()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                        Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                    }
                }
            };
        }


    }
}

