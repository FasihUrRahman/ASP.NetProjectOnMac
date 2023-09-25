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
        public void DeleteCategory(int id)
        {
            Category categoryId = _db.Categories.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _db.Remove(categoryId);
            _db.SaveChanges();

        }
        //----Post Methods Defination----//
        public Post GetPost(int id)
        {
            return _db.Posts.Where(x => x.Id.Equals(id)).Include(x => x.Category).Include(x => x.PostStatus).Include(x => x.User).FirstOrDefault();
        }

        public List<Post> GetPosts
        {
            get { return _db.Posts.Include(x => x.Category).Include(x => x.PostStatus).ToList(); }
        }
    }
}