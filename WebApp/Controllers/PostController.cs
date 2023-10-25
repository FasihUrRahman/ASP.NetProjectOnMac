using System.Drawing;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostController(IPost post, IUserAccount account, IWebHostEnvironment webHostEnvironment)
        {
            _post = post;
            _account = account;
            _webHostEnvironment = webHostEnvironment;
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
            ViewBag.AllPostStatuses = new SelectList(_post.GetPostStatuses().ToList(), "Id", "Name");
            return View(_post.GetPost(id));
        }
        //User user = new CommonController(_account).GetUser(HttpContext);
        [HttpPost]
        public IActionResult AddEditPost(Post post, IFormFile PostImage)
        {
            if (PostImage != null)
            {
                string folderPath = "images/posts/";
                folderPath += Guid.NewGuid().ToString()+"_"+PostImage.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
                PostImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                //Set The Velue of Post Image
                post.Image = folderPath;
                //Getting Value of User From Common Controller
                User user = new CommonController(_account).GetUser(HttpContext);
                post.UserId = user.Id;  //Giving Value of Common Controller to user
                try
                {
                    _post.AddEditPost(post);
                    return RedirectToAction("GetPosts");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"An Error Comes While Saving The Post In DB \'{ex}\'";
                    return View(post);
                }
            }
            //For Windows Image Processing
            /*
            string imagePath = "";
            var extention = "";
            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            int maxContentLength = 1024 * 1024 * 10;
            if(PostImage != null && PostImage.Length > 0)
            {
                extention = PostImage.FileName.Substring(PostImage.FileName.LastIndexOf(".")).ToLower();
                if (PostImage.Length > maxContentLength)
                {
                    ViewBag.Error = "File size must be less than 10MB";
                }
                else if (!AllowedFileExtensions.Contains(extention))
                {
                    ViewBag.Error = "Please Upload Image of Type .jpg .jpeg .png";
                }
                else
                {
                    string relativeImagePath = $"/images/posts/{post.Id}-{Path.GetFileNameWithoutExtension(PostImage.FileName)}-{DateTime.UtcNow.Ticks}{extention}";
                    string absoluteImagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{relativeImagePath}");
                    using (var stream = new FileStream(absoluteImagePath, FileMode.Create))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            PostImage.CopyTo(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int width = img.Width;
                                int height = img.Height;
                                if (width > 800 || height > 800)
                                {
                                    ViewBag.Error = "Please Upload image with dimenstions \'700\' x \'700\' or less";
                                }
                                else
                                {
                                    PostImage.CopyTo(stream);
                                    post.Image = relativeImagePath;
                                    //Getting Value of User From Common Controller
                                    User user = new CommonController(_account).GetUser(HttpContext);
                                    post.UserId = user.Id;  //Giving Value of Common Controller to user

                                    try
                                    {
                                        _post.AddEditPost(post);
                                        return RedirectToAction("GetPosts");
                                    }
                                    catch (Exception ex)
                                    {
                                        ViewBag.Error = $"An Error Comes While Saving The Post In DB \'{ex}\'";
                                        return View(post);
                                    }
                                }
                            }
                        }
                    }
                }
            }*/
            return View(post);
        }
        [HttpGet]
        public IActionResult DeletePost(int id)
        {
            _post.DeletePost(id);
            return RedirectToAction("GetPosts");
        }
        //------Reaction Types------//
        [Admin]
        [HttpGet]
        public IActionResult ReactionTypes()
        {
            return View(_post.GetReactionTypes());
        }
        //AddEditReactionType
        [HttpGet]
        public IActionResult AddEditReactionType(int id = 0)
        {
            return View(_post.GetReactionType(id));
        }
        [HttpPost]
        public IActionResult AddEditReactionType(ReactionType reactionType)
        {
            _post.AddEditReactionType(reactionType);
            return RedirectToAction("ReactionTypes");
        }
        [HttpGet]
        public IActionResult DeleteReactionType(int id)
        {
            _post.DeleteReactionType(id);
            return RedirectToAction("ReactionTypes");
        }
        //-----Post Status-----//
        [Admin]
        [HttpGet]
        public IActionResult PostStatus()
        {
            return View(_post.GetPostStatuses());
        }
        //Getting PostStatus by ID
        [HttpGet]
        public IActionResult AddEditPostStatus(int id = 0)
        {
            return View(_post.GetPostStatus(id));
        }
        [HttpPost]
        public IActionResult AddEditPostStatus(PostStatus postStatus)
        {
            _post.AddEditPostStatus(postStatus);
            return RedirectToAction("PostStatus");
        }
        [HttpGet]
        public IActionResult DeletePostStaus(int id)
        {
            _post.DeleteReactionType(id);
            return RedirectToAction("PostStatus");
        }
    }
}