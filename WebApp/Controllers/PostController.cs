using Blog.Models;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _post;
        private readonly IUserAccount _account;
        public PostController(IPost post, IUserAccount account)
        {
            _post = post;
            _account = account;
        }
        //------Categories Methods------//

        //Getting Categories From DB
        [Admin]
        [HttpGet]
        public IActionResult Categories()
        {
            ViewBag.Message = TempData["Message"] as string;
            return View(_post.GetCategories());
        }
        //Getting Category by id from DB in AddEditCategory Page
        [HttpGet]
        public IActionResult AddEditCategory(int id = 0)
        {
            return View(_post.GetCategory(id));
        }

        //Add Category To DB from User Side
        [HttpPost]
        public IActionResult AddEditCategory(Category category)
        {
            _post.AddEditCategory(category);
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            TempData["Message"] = _post.DeleteCategory(id);
            return RedirectToAction("Categories");
        }
        //------Post Methods------//

        //Getting Post From DB
        [Admin]
        [HttpGet]
        public IActionResult GetPosts()
        {
            return View(_post.GetPosts);
        }

        //Getting Post From DB By ID
        [Admin]
        [HttpGet]
        public IActionResult DetailsPost(int id)
        {
            return View(_post.GetPost(id));
        }

        [HttpGet]
        public IActionResult AddEditPost(int id)
        {
            ViewBag.AllCategories = new SelectList(_post.GetCategories().ToList(), "Id", "Name");
            return View(_post.GetPost(id));
        }
        //User user = new CommonController(_account).GetUser(HttpContext);
        [HttpPost]
        public IActionResult AddEditPost(Post post)
        {
            //Getting Value of User From Common Controller
            User user = new CommonController(_account).GetUser(HttpContext);
            post.UserId = user.Id;  //Giving Value of Common Controller to user
            _post.AddEditPost(post);    //Calling Method for Sending data to DB
            return RedirectToAction("GetPosts");
        }
        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            _post.DeletePost(id);
            return RedirectToAction("GetPosts");
        }
    }
}