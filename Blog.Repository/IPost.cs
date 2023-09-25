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
        List<Category> GetCategories();
        Category GetCategory(int id);
        void AddEditCategory(Category category);
        void DeleteCategory(int id);
        //----------Posts----------//
        List<Post> GetPosts { get; }
        Post GetPost(int id);
    }
}