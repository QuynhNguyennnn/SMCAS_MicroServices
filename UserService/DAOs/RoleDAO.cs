using UserService.Models;

namespace UserService.DAOs
{
    public class RoleDAO
    {
        public static Role GetRoleById(int id)
        {
            Role role = new Role();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    role = context.Roles.FirstOrDefault(r => r.RoleId == id);
                    if (role != null)
                    {
                        return role;
                    } else
                    {
                        return null;
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Role CreateRole(Role role)
        {
            Role newRole = new Role();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var roleCheck = context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.RoleName.ToLower());
                    if (roleCheck != null)
                    {
                        return null;
                    } else
                    {
                        newRole.RoleName = role.RoleName;
                        newRole.IsActive = true;
                        context.Roles.Add(newRole);
                        context.SaveChanges();
                    }
                }
                return newRole;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var roleList = context.Roles.ToList();
                    foreach (var movie in roleList)
                    {
                        if (movie.IsActive)
                        {
                            roles.Add(movie);
                        }
                    }
                }
                return roles;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Role UpdateRole(Role role)
        {
            Role updatedRole = new Role();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var roleCheck = context.Roles.FirstOrDefault(r => r.RoleId == role.RoleId && r.IsActive);
                    if (roleCheck != null)
                    {
                        updatedRole.RoleId = roleCheck.RoleId;
                        updatedRole.RoleName = role.RoleName;
                        updatedRole.IsActive = role.IsActive;
                        context.Entry(roleCheck).CurrentValues.SetValues(updatedRole);
                        context.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                    return updatedRole;
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Role DeleteRole(int roleId)
        {
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var role = context.Roles.FirstOrDefault(r => r.RoleId == roleId);
                    if (role == null)
                    {
                        return null;
                    } else
                    {
                        role.IsActive = false;
                        context.Roles.Update(role);
                        context.SaveChanges();
                    }
                    return role;
                }
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Role> SearchRoleByName(string roleName)
        {
            var roleList = new List<Role>();
            try
            {
                using (var context = new SepprojectDbV2Context())
                {
                    var roleCheck = context.Roles.Where(r => r.RoleName.ToLower().Contains(roleName.ToLower()) && r.IsActive).ToList();
                    if (roleCheck != null)
                    {
                        foreach (var item in roleCheck)
                        {
                            roleList.Add(item);
                        }
                        return roleList;
                    }
                    else
                    {
                        return null;
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
