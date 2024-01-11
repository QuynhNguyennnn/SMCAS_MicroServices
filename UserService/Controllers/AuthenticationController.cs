﻿using AutoMapper;
using UserService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Models;
using UserService.Services;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserService userService = new Services.UserService();
        private IRoleService roleService = new RoleService();
        public AuthResponse authResponse = new AuthResponse();

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<ServiceResponse<AuthResponse>> Register(RegisterRequest registerRequest)
        {
            ServiceResponse<AuthResponse> serviceResponse = new ServiceResponse<AuthResponse>();
            var usernameValidate = userService.GetUserByUsername(registerRequest.Username);
            if (usernameValidate != null)
            {
                serviceResponse.Message = "Username already exists.";
                return BadRequest(serviceResponse);
            }
            else
            {
                if (registerRequest.Password != null && registerRequest.Password == registerRequest.ConfirmPassword)
                {
                    var passwordHash = HashPassword(registerRequest.Password);  
                    var userMap = _mapper.Map<User>(registerRequest);
                    userMap.Password = passwordHash;
                    var user = userService.Register(userMap);
                    var token = GenerateAccessToken(user.Username);
                    authResponse.UserId = user.UserId;
                    authResponse.UserName = user.Username;
                    authResponse.AccessToken = token;
                    serviceResponse.Data = authResponse;
                    serviceResponse.Status = 200;
                    serviceResponse.Message = "Register successful";
                    return Ok(serviceResponse);
                }
                else
                {
                    serviceResponse.Message = "Password and Password Confirm not matched.";
                    serviceResponse.Status = 400;
                    return BadRequest(serviceResponse);
                }
            }
        }

        [HttpPost("login")]
        public ActionResult<ServiceResponse<AuthResponse>> Login(LoginRequest loginDto)
        {
            var serviceResponse = new ServiceResponse<AuthResponse>();
            var user = userService.GetUserByUsername(loginDto.Username);
            if (user == null)
            {
                serviceResponse.Message = "Username not found.";
                return NotFound(serviceResponse);
            }
            else
            {
                var hash = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
                if (!hash)
                {
                    serviceResponse.Message = "Password not correct.";
                    return NotFound(serviceResponse);
                }
                else
                {
                    var token = GenerateAccessToken(loginDto.Username);
                    authResponse.AccessToken = token;
                    var refreshToken = GenerateRefreshToken(loginDto.Username);
                    authResponse.UserId = user.UserId;
                    authResponse.UserName = loginDto.Username;
                    authResponse.RefreshToken = refreshToken;
                    serviceResponse.Message = "Login successful";
                    serviceResponse.Data = authResponse;
                    serviceResponse.Status = 200;
                    serviceResponse.TotalDataList = 1;
                    return Ok(serviceResponse);
                }
            }
        }

        [HttpPost("refreshToken")]
        public ActionResult<ServiceResponse<AuthResponse>> RefreshToken(string refreshToken)
        {
            var serviceResponse = new ServiceResponse<AuthResponse>();
            var tokenReader = GetTokenInfor(refreshToken);
            if (userService.GetUserByUsername(tokenReader.Username) != null && tokenReader.ExpireDate > DateTime.Now)
            {

                authResponse.AccessToken = GenerateAccessToken(tokenReader.Username);
                authResponse.RefreshToken = GenerateRefreshToken(tokenReader.Username);
                serviceResponse.Data = authResponse;
                serviceResponse.Status = 200;
                serviceResponse.TotalDataList = 1;
                serviceResponse.Message = "Token is recreated.";
            }
            else
            {
                serviceResponse.Data = null;
                serviceResponse.Status = 400;
                serviceResponse.TotalDataList = 0;
                serviceResponse.Message = "Invalid Refresh token";
                return Unauthorized(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [NonAction]
        private string GenerateAccessToken(string username)
        {
            var user = userService.GetUserByUsername(username);
            var role = roleService.GetRoleById(user.RoleId);
            List<Claim> claims = new List<Claim>
            {
                new Claim("role", role.RoleName),
                new Claim("User", role.User),
                new Claim("Blog", role.Blog),
                new Claim("Medicine", role.Medicine),
                new Claim("ExaminatedRecord", role.ExaminatedRecord),
                new Claim("Feedback", role.Feedback),
                new Claim("Schedule", role.Schedule),
                new Claim("Chat", role.Chat),
                new(JwtRegisteredClaimNames.Name, username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSetting:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [NonAction]
        private string GenerateRefreshToken(string username)
        {
            var user = userService.GetUserByUsername(username);
            var role = roleService.GetRoleById(user.RoleId);
            List<Claim> claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSetting:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [NonAction]
        public TokenReaderResponse GetTokenInfor(string token)
        {
            TokenReaderResponse reader = new TokenReaderResponse();
            var jwtToken = new JwtSecurityToken(token);
            var nameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "name");
            var expirationDate = jwtToken.ValidTo;
            if (nameClaim != null)
            {
                reader.Username = nameClaim.Value;
                reader.ExpireDate = expirationDate.AddHours(7);
                var userInfor = userService.GetUserByUsername(reader.Username);
                var userRole = roleService.GetRoleById(userInfor.RoleId);
                reader.Role = userRole.RoleName;
            }
            return reader;
        }

        [NonAction]
        public string HashPassword(string password)
        {
            if (password == null)
            {
                return null;
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        [HttpGet]
        [Authorize(Policy = "FullAccess")]
        public ActionResult<ServiceResponse<List<UserResponse>>> GetUsers()
        {
            var response = new ServiceResponse<List<UserResponse>>();
            var userResponseList = new List<UserResponse>();
            var userList = userService.GetUsers();
            foreach (var user in userList)
            {
                userResponseList.Add(_mapper.Map<UserResponse>(user));
            }
            response.Data = userResponseList;
            response.Message = "Get User List";
            response.Status = 200;
            response.TotalDataList = userResponseList.Count;
            return response;
        }

        [HttpGet("id")]
        [Authorize(Policy = "UserUpdateOrFullAccess")]
        public ActionResult<ServiceResponse<UserResponse>> GetUserById(int id)
        {
            var response = new ServiceResponse<UserResponse>();
            var user = userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var roleList = roleService.GetRoles();
            var userResponse = _mapper.Map<UserResponse>(user);
            foreach (var role in roleList)
            {
                if (user.RoleId == role.RoleId)
                {
                    userResponse.RoleName = role.RoleName;
                    break;
                }
            }
            response.Data = userResponse;
            response.Message = "Get User List";
            response.Status = 200;
            return response;
        }

        [HttpPut("id")]
        [Authorize(Policy = "FullAccess")]
        public ActionResult<ServiceResponse<UserResponse>> DeleteUser(int id)
        {
            var response = new ServiceResponse<List<UserResponse>>();
            var user = userService.DeleteUser(id);
            if (user == null)
            {
                response.Message = "User not found.";
                response.Status = 404;
                return NotFound(response);
            }
            response.Message = "User deleted.";
            response.Status = 200;
            return Ok(response);
        }

        [HttpPut("Update")]
        [Authorize(Policy = "UserUpdateOrFullAccess")]
        public ActionResult<ServiceResponse<UserResponse>> UpdateUser(UpdateUserRequest updateRequest)
        {
            var response = new ServiceResponse<UserResponse>();
            var userValidation = userService.GetUserById(updateRequest.UserId);
            if (userValidation == null)
            {
                response.Status = 404;
                response.Message = "User not exists.";
                return NotFound(response);
            }
            var passwordHash = HashPassword(updateRequest.Password);
            updateRequest.Password = passwordHash;
            var userMap = _mapper.Map<User>(updateRequest);
            var user = userService.UpdateUser(userMap);
            if (user == null)
            {
                response.Status = 404;
                response.Message = "User not exists.";
                return NotFound(response);
            }
            else
            {
                var userUpdated = _mapper.Map<UserResponse>(user);
                response.Status = 200;
                response.Data = userUpdated;
                response.Message = "Updated successful";
                response.TotalDataList = 1;
            }
            return Ok(response);
        }

        [HttpGet("Search/name")]
        [Authorize(Policy = "FullAccess")]
        public ActionResult<ServiceResponse<List<UserResponse>>> SearchUserByName(string name)
        {
            var response = new ServiceResponse<List<UserResponse>>();
            var userResponseList = new List<UserResponse>();
            var userList = userService.SearchUserByName(name);
            foreach (var user in userList)
            {
                userResponseList.Add(_mapper.Map<UserResponse>(user));
            }
            response.Data = userResponseList;
            response.Message = "List user have name contain: " + name;
            response.Status = 200;
            response.TotalDataList = userResponseList.Count;
            if (userResponseList.Count == 0)
            {
                response.Data = userResponseList;
                response.Message = "There is no user name: " + name;
                response.Status = 404;
                response.TotalDataList = userResponseList.Count;
            }
            return response;
        }

        [HttpGet("Doctors")]
        [Authorize(Policy = "FullAccess")]
        public ActionResult<ServiceResponse<List<DoctorResponse>>> GetDoctors()
        {
            var response = new ServiceResponse<List<DoctorResponse>>();
            var userResponseList = new List<DoctorResponse>();
            var userList = userService.GetDoctors();
            var r = 1;
            foreach (var user in userList)
            {
                var userRe = _mapper.Map<DoctorResponse>(user);
                userRe.Key = r;
                userResponseList.Add(userRe);
                r++;
            }
            response.Data = userResponseList;
            response.Message = "Get Doctors List";
            response.Status = 200;
            response.TotalDataList = userResponseList.Count;
            return response;
        }

        [HttpGet("Students")]
        [Authorize(Policy = "FullAccess")]
        public ActionResult<ServiceResponse<List<StudentResponse>>> GetStudents()
        {
            var response = new ServiceResponse<List<StudentResponse>>();
            var userResponseList = new List<StudentResponse>();
            var userList = userService.GetStudents();
            var r = 1;
            foreach (var user in userList)
            {
                var userRe = _mapper.Map<StudentResponse>(user);
                userRe.Key = r;
                userResponseList.Add(userRe);
                r++;
            }
            response.Data = userResponseList;
            response.Message = "Get Student List";
            response.Status = 200;
            response.TotalDataList = userResponseList.Count;
            return response;
        }

        [HttpGet("Users")]
        [Authorize(Policy = "FullAccess")]
        public ActionResult<ServiceResponse<List<UserManagementResponse>>> GetUserList()
        {
            var response = new ServiceResponse<List<UserManagementResponse>>();
            var userResponseList = new List<UserManagementResponse>();
            var userList = userService.GetUsers();
            var roleList = roleService.GetRoles();
            var r = 1;
            foreach (var user in userList)
            {
                var userRe = _mapper.Map<UserManagementResponse>(user);
                userRe.Key = r;

                foreach (var role in roleList)
                {
                    if (user.RoleId == role.RoleId)
                    {
                        userRe.RoleName = role.RoleName;
                        break;
                    }
                }
                userResponseList.Add(userRe);
                r++;
            }
            response.Data = userResponseList;
            response.Message = "Get User List";
            response.Status = 200;
            response.TotalDataList = userResponseList.Count;
            return response;
        }

        [HttpGet("Staffs")]
        [Authorize(Policy = "FullAccess")]
        public ActionResult<ServiceResponse<List<StaffResponse>>> GetStaff()
        {
            var response = new ServiceResponse<List<StaffResponse>>();
            var userResponseList = new List<StaffResponse>();
            var userList = userService.GetStaffs();
            var roleList = roleService.GetRoles();
            var r = 1;
            foreach (var user in userList)
            {
                var userRe = _mapper.Map<StaffResponse>(user);
                userRe.Key = r;

                foreach (var role in roleList)
                {
                    if (user.RoleId == role.RoleId)
                    {
                        userRe.RoleName = role.RoleName;
                        userRe.RoleId = role.RoleId;
                        break;
                    }
                }
                userResponseList.Add(userRe);
                r++;
            }
            response.Data = userResponseList;
            response.Message = "Get Staff List";
            response.Status = 200;
            response.TotalDataList = userResponseList.Count;
            return response;
        }

        [HttpPost("Create")]
        [Authorize(Policy = "FullAccess")]
        public ActionResult<ServiceResponse<UserResponse>> CreateUser(CreateUserRequest request)
        {
            var response = new ServiceResponse<UserResponse>();
            var userMap = _mapper.Map<User>(request);
            var roleCheck = roleService.GetRoleById(request.RoleId);
            if (roleCheck != null)
            {
                var passwordHash = HashPassword(request.Password);
                userMap.Password = passwordHash;
                var usernameValidate = userService.GetUserByUsername(request.Username);
                if (usernameValidate != null)
                {
                    response.Message = "Username already exists.";
                    response.Status = 400;
                    return BadRequest(response);
                }
                else
                {
                    var userCreated = userService.CreateUser(userMap);
                    if (userCreated != null)
                    {
                        var user = _mapper.Map<UserResponse>(userCreated);
                        response.Data = user;
                        response.Message = "Create user successful.";
                        response.Status = 200;
                        response.TotalDataList = 1;
                    }
                    else
                    {
                        response.Data = null;
                        response.Message = "Create user failed.";
                        response.Status = 400;
                        response.TotalDataList = 0;
                    }
                }
            }
            else
            {
                response.Data = null;
                response.Message = "Role not found.";
                response.Status = 404;
                response.TotalDataList = 0;
            }
            return response;
        }

        [HttpGet("Patients")]
        [Authorize(Policy = "UserViewOrFullAccess")]
        public ActionResult<ServiceResponse<List<UserResponse>>> GetPatientList()
        {
            var response = new ServiceResponse<List<UserResponse>>();
            var userResponseList = new List<UserResponse>();
            var patientsList = userService.GetPatientList();
            foreach ( var patient in patientsList )
            {
                userResponseList.Add(_mapper.Map<UserResponse>(patient));

            }
            if (userResponseList.Count > 0)
            {
                response.Data = userResponseList;
                response.Status = 200;
                response.Message = "Get Patients List";
                response.TotalDataList = userResponseList.Count;
            } else
            {
                response.Data = null;
                response.Status = 204;
                response.Message = "No patients";
                response.TotalDataList = 0;
            }
            return response;
        }
    }
}
