using Microsoft.Data.SqlClient;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }

        public Comment GetCommentsByPostId(int id, int PostId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.PostId, c.UserProfileId, c.Subject, c.Content, c.CreateDateTime,
                              p.Id,
                              u.DisplayName, 
                              u.UserTypeId, 
                              ut.[Name] AS UserTypeName
                         FROM Comment C
                              LEFT JOIN Post p ON c.PostId = p.id
                              LEFT JOIN UserProfile u ON c.UserProfileId = u.id
                              LEFT JOIN UserType ut ON c.UserTypeId = ut.id
                        WHERE c.id = @id AND c.PostId = @PostId
                       ";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@PostId", PostId);
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
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
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
