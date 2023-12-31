﻿using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Implimentation
{
    public class UserRepository : IUser
    {
        private readonly BlogContext _db; //This will get Values From BlogContext and have all the DB Valuess
        //Database Connection in Constructor
        public UserRepository(BlogContext db)
        {
            _db = db;
        }
        //-------User Role Methods------//
        public void AddEditRole(UserRole userRole)
        {
            _db.UsersRole.Update(userRole);
            _db.SaveChanges();
        }
        public string DeleteRole(int id)
        {
            UserRole userRoleId = _db.UsersRole.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (GetUser().UserRoleId != id)
            {
                _db.Remove(userRoleId);
                _db.SaveChanges();
                return "Delete Successfully";
            }
            return "Unable to Delete as it's already in Use. Please Delete Users before deleteing Role";

        }
        public UserRole GetRole(int id)
        {
            return _db.UsersRole.Where(x => x.Id == id).FirstOrDefault();
        }
        public List<UserRole> GetRoles()
        {
            return _db.UsersRole.ToList();
        }
        //-----User Methods------//
        public List<User> GetUsers()
        {
            return _db.Users.Include(x => x.UserRole).ToList();
        }
        public User GetUser()
        {
            return _db.Users.Include(x => x.UserRole).FirstOrDefault();
        }
        public User GetUser(int id)
        {
            return _db.Users.Where(x => x.Id == id).Include(x => x.UserRole).FirstOrDefault();
        }

        public void AddEditUser(User user)
        {
            if (!string.IsNullOrEmpty(user.AccessToken))
            {
                _db.Users.Update(user);
            }
            else
            {
                user.JoinedOn = DateTime.UtcNow.AddHours(5);
                user.AccessToken = Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks;
                _db.Users.Update(user);
            }
            _db.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User userId = _db.Users.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _db.Remove(userId);
            _db.SaveChanges();
        }
    }
}