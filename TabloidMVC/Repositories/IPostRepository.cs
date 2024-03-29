﻿using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void EditPost(Post post);
        void DeletePost(int id);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        Post GetPostWithComments(int id);
        Post GetUserPostById(int id, int userProfileId);
    }
}