using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public interface IUserAccount
    {
        //string Register(User user);
        //void GetUserInfo();
        //To Get Token
        User GetUserForLogin(string email, string password);
        string Register(User user);
        //Getting Value For Common Controller
        User GetUserInfo(string accessToken);
    }
}