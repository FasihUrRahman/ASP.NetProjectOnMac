using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public interface IUser
    {
        //To Get Roles From DB
        //User Role Methods
        List<UserRole> GetRoles();
        UserRole GetRole(int id);
        void AddEditRole(UserRole userRole);
        string DeleteRole(int id);
        //To Get Users From DB
        //Users Mehtods
        List<User> GetUsers();
        User GetUser();
        User GetUser(int id);
        void AddEditUser(User user);
        void DeleteUser(int id);
    }
}