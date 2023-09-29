using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class CommonController : Controller
    {
        private readonly IUserAccount _account;
        public CommonController(IUserAccount account)
        {
            _account = account;
        }
        public User GetUser(HttpContext context)
        {
            string cookie = context.Request.Cookies["user-access-token"];
            if (cookie != null)
            {
                User user = _account.GetUserInfo(cookie);
                if(user != null)
                {
                    return user;
                }
            }
            return null;
        }
    }
}

