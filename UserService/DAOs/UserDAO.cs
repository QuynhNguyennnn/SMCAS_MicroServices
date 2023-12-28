using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UserService.Models;

namespace UserService.DAOs
{
    public class UserDAO
    {
        public static bool Login(string username, string password)
        {
            User user = new User();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    user = context.Users.FirstOrDefault(user => (user.Username == username) && user.IsActive);
                    if (user != null)
                    {
                        if (user.Password == password)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static User GetUserByUsername(string username)
        {
            User member = new User();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    member = context.Users.FirstOrDefault(user => (user.Username == username) && user.IsActive);
                    if (member != null)
                    {
                        return member;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static User Register(User user)
        {
            User userNew = new User();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    var userValidate = context.Users.FirstOrDefault(userCheck => userCheck.Username == user.Username);
                    if (userValidate != null)
                    {
                        return null;
                    }
                    else
                    {
                        var datetime = DateTime.Now;
                        userNew.FirstName = user.FirstName;
                        userNew.LastName = user.LastName;
                        userNew.PhoneNumber = user.PhoneNumber;
                        userNew.Username = user.Username;
                        userNew.Password = user.Password;
                        userNew.Birthday = datetime;
                        userNew.Address = "Unidentified";
                        userNew.Gender = "Unknown";
                        userNew.RoleId = 2;
                        userNew.IsActive = true;
                        context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
                        context.Users.Add(userNew);
                        context.SaveChanges();
                        return userNew;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<User> GetUsers()
        {
            var users = new List<User>();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    var userList = context.Users.ToList().Where(u => u.IsActive);
                    foreach (var user in userList)
                    {
                        users.Add(user);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return users;
        }

        public static User GetUserById(int id)
        {
            var user = new User();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    user = context.Users.FirstOrDefault(user => (user.UserId == id) && user.IsActive);
                    if (user != null)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static User DeleteUser(int id)
        {
            var userActive = new User();
            var userUnActive = new User();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    userActive = context.Users.FirstOrDefault(user => (user.UserId == id) && user.IsActive);
                    userUnActive = context.Users.FirstOrDefault(user => (user.UserId == id) && !user.IsActive);
                    if (userActive != null)
                    {
                        userActive.IsActive = false;
                        context.Users.Update(userActive);
                        context.SaveChanges();
                        return userActive;
                    }
                    else if (userUnActive != null)
                    {
                        return userUnActive;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static User UpdateUser(User user)
        {
            var userUpdate = new User();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    userUpdate = context.Users.FirstOrDefault(u => u.UserId == user.UserId);
                    if (userUpdate == null)
                    {
                        return null;
                    }
                    else
                    {
                        userUpdate.Username = user.Username;
                        userUpdate.Password = user.Password;
                        userUpdate.FirstName = user.FirstName;
                        userUpdate.LastName = user.LastName;
                        userUpdate.RoleId = user.RoleId;
                        userUpdate.PhoneNumber = user.PhoneNumber;
                        userUpdate.Address = user.Address;
                        userUpdate.Birthday = user.Birthday;
                        userUpdate.Gender = user.Gender;
                        userUpdate.Major = user.Major;
                        userUpdate.Experience = user.Experience;
                        userUpdate.WorkPlace = user.WorkPlace;
                        userUpdate.Qualification = user.Qualification;
                        userUpdate.EmergencyContact = user.EmergencyContact;
                        userUpdate.Course = user.Course;
                        userUpdate.StudentCode = user.StudentCode;
                        userUpdate.University = user.University;
                        userUpdate.IsActive = user.IsActive;
                        context.Users.Update(userUpdate);
                        context.SaveChanges();
                    }
                    return userUpdate;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<User> SearchUserByName(string name)
        {
            var userList = new List<User>();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    var userCheck = context.Users.Where(u => (u.FirstName.ToLower().Contains(name.ToLower())
                                                                || u.LastName.ToLower().Contains(name.ToLower()))
                                                                && u.IsActive).ToList();
                    if (userCheck != null)
                    {
                        foreach (var item in userCheck)
                        {
                            userList.Add(item);
                        }
                        return userList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<User> GetDoctors()
        {
            List<User> users = new List<User>();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    var doctorList = context.Users.Where(u => u.RoleId == 1 && u.IsActive);
                    foreach (var doctor in doctorList)
                    {
                        users.Add(doctor);
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<User> GetStudents()
        {
            List<User> users = new List<User>();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    var studnetList = context.Users.Where(u => u.RoleId == 2 && u.IsActive);
                    foreach (var student in studnetList)
                    {
                        users.Add(student);
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<User> GetStaffs()
        {
            List<User> users = new List<User>();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    var studnetList = context.Users.Where(u => (u.RoleId == 3 || u.RoleId == 4) && u.IsActive);
                    foreach (var student in studnetList)
                    {
                        users.Add(student);
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static User CreateUser(User user)
        {
            var userNew = new User();
            try
            {
                using (var context = new SepprojectDbV4Context())
                {
                    var userCheck = context.Users.FirstOrDefault(u => u.Username == user.Username);
                    if (userCheck == null)
                    {
                        userNew = user;
                        userNew.IsActive = true;
                        context.Users.Add(userNew);
                        context.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                }
                return userNew;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
