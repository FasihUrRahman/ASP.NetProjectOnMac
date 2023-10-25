using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public interface IPost
    {
        //------Category-----//
        List<Category> GetCategories();
        Category GetCategory(int id);
        void AddEditCategory(Category category);
        string DeleteCategory(int id);
        //----------Posts----------//
        List<Post> GetPosts { get; }
        public Post GetPost();
        Post GetPost(int id);
        void AddEditPost(Post post);
        void DeletePost(int id);
        //------Reaction types-----//
        List<ReactionType> GetReactionTypes();
        ReactionType GetReactionType(int id);
        void AddEditReactionType(ReactionType reactionType);
        void DeleteReactionType(int id);
        //-----Post Status-----//
        List<PostStatus> GetPostStatuses();
        PostStatus GetPostStatus(int id);
        void AddEditPostStatus(PostStatus postStatus);
        void DeletePostStatus(int id);
    }
}