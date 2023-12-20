using UserService.DAOs;
using UserService.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        public User DeleteUser(int id) => UserDAO.DeleteUser(id);

        public List<User> GetDoctors() => UserDAO.GetDoctors();

        public List<User> GetStudents() => UserDAO.GetStudents();

        public User GetUserById(int id) => UserDAO.GetUserById(id);

        public User GetUserByUsername(string username) => UserDAO.GetUserByUsername(username);

        public List<User> GetUsers() => UserDAO.GetUsers();

        public bool Login(string username, string password) => UserDAO.Login(username, password);

        public User Register(User user) => UserDAO.Register(user);

        public List<User> SearchUserByName(string name) => UserDAO.SearchUserByName(name);

        public User UpdateUser(User user) => UserDAO.UpdateUser(user);
    }
}
