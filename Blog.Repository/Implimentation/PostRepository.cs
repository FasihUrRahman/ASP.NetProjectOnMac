using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Implimentation
{
    public class PostRepository : IPost
    {
        private readonly BlogContext _db;   //DB Connection
        public PostRepository(BlogContext db)   //Bring all the Models from DB
        {
            _db = db;
        }
        //----Category Method Defination----//
        //Geting Category From DB in Categories Page
        public List<Category> GetCategories()
        {
            return _db.Categories.ToList();
        }

        //Geting Category by Id From DB in AddEditCategory Page
        public Category GetCategory(int id)
        {
            return _db.Categories.Where(x => x.Id == id).FirstOrDefault();
        }

        //Adding Category to DB from User Side
        public void AddEditCategory(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }

        //Delete Category from DB
        public string DeleteCategory(int id)
        {
            Category categoryId = _db.Categories.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (GetPost().CategoryId != id)
            {
                _db.Remove(categoryId);
                _db.SaveChanges();
                return "Successfully Deleted";
            }
            return "Unable to Delete Category. This is already Used by Posts";
        }
        public Post GetPost()
        {
            return _db.Posts.Include(x => x.Category).Include(x => x.PostStatus).FirstOrDefault();
        }
        //----Post Methods Defination----//
        public Post GetPost(int id)
        {
            return _db.Posts.Where(x => x.Id.Equals(id)).Include(x => x.Category).Include(x => x.PostStatus).Include(x => x.User).FirstOrDefault();
        }
        //Delete Post
        public void DeletePost(int id)
        {
            Post post = _db.Posts.Where(x => x.Id == id).FirstOrDefault();
            _db.Posts.Remove(post);
            _db.SaveChanges();
        }
        //Add Update Post
        public void AddEditPost(Post post)
        {
            post.PostedOn = DateTime.UtcNow.AddHours(5);
            _db.Update(post);
            _db.SaveChanges();
        }
        //Get Reactions Type in List
        public List<ReactionType> GetReactionTypes()
        {
            return _db.ReactionTypes.ToList();
        }
        //Get Reaction Type by ID
        public ReactionType GetReactionType(int id)
        {
            return _db.ReactionTypes.Where(x => x.Id == id).FirstOrDefault();
        }
        //Add or Update Reaction Type
        public void AddEditReactionType(ReactionType reactionType)
        {
            _db.Update(reactionType);
            _db.SaveChanges();
        }
        //Delete Reaction Type
        public void DeleteReactionType(int id)
        {
            ReactionType reactionTypeId = _db.ReactionTypes.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _db.Remove(reactionTypeId);
            _db.SaveChanges();
        }
        //Get Post Statuses
        public List<PostStatus> GetPostStatuses()
        {
            return _db.PostsStatus.ToList();
        }
        //Get PostStatus By ID
        public PostStatus GetPostStatus(int id)
        {
            return _db.PostsStatus.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
        //Set Value to DB from Given Data
        public void AddEditPostStatus(PostStatus postStatus)
        {
            _db.Update(postStatus);
            _db.SaveChanges();
        }
        //Remove Value from DB by ID
        public void DeletePostStatus(int id)
        {
            PostStatus postStatusId = _db.PostsStatus.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _db.Remove(postStatusId);
            _db.SaveChanges();
        }
        //Read Post
        public List<Post> GetPosts
        {
            get { return _db.Posts.Include(x => x.Category).Include(x => x.PostStatus).ToList(); }
        }

    }
}