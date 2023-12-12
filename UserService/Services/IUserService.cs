﻿using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        bool Login(string username, string password);
        User GetUserByUsername(string username);
        User Register(User user);
        List<User> GetUsers();
        User GetUserById(int id);
        User DeleteUser(int id);
        User UpdateUser(User user);
    }
}
