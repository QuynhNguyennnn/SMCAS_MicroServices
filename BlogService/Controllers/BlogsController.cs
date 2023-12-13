using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogService.Models;
using BlogService.Services;
using AutoMapper;
using BlogService.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace BlogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly HttpClient _httpClient = null;
        private IBlogService service = new Services.BlogService();
        private readonly IMapper _mapper;

        public BlogsController(IMapper mapper)
        {
            _httpClient = new HttpClient();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ServiceResponse<List<BlogResponse>>> GetBlogList()
        {
            var response = new ServiceResponse<List<BlogResponse>>();
            var blogResponseList = new List<BlogResponse>();
            var blogList = service.GetBlogList();
            foreach (var movie in blogList)
            {
                BlogResponse blogResponse = _mapper.Map<BlogResponse>(movie);
                blogResponseList.Add(blogResponse);
            }

            response.Data = blogResponseList;
            response.Message = "Get Blog List";
            response.Status = 200;
            response.TotalDataList = blogResponseList.Count;
            return response;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ServiceResponse<BlogResponse>>> GetBlogById(int id)
        {
            var blog = service.GetBlogById(id);
            var blogResponse = _mapper.Map<BlogResponse>(blog);

            var response = new ServiceResponse<BlogResponse>();
            response.Data = blogResponse;
            response.Message = "Get Blog Detail";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Medical Staff, Admin")]
        public ActionResult<ServiceResponse<BlogResponse>> CreateBlog(AddBlogRequest addBlog)
        {
            Blog blog = _mapper.Map<Blog>(addBlog);
            blog = service.CreateBlog(blog);
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            var response = new ServiceResponse<BlogResponse>();
            response.Data = blogResponse;
            response.Message = "Create Blog";
            response.Status = 200;
            return response;
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Medical Staff, Admin")]
        public ActionResult<ServiceResponse<BlogResponse>> UpdateBlog(UpdateBlogResquest updateBlog)
        {
            Blog blog = _mapper.Map<Blog>(updateBlog);

            blog = service.UpdateBlog(blog);
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            var response = new ServiceResponse<BlogResponse>();
            response.Data = blogResponse;
            response.Message = "Update Blog";
            response.Status = 200;
            return response;
        }

        [HttpPut("Delete")]
        [Authorize(Roles = "Medical Staff, Admin")]
        public ActionResult<ServiceResponse<BlogResponse>> DeleteBlog(int id)
        {
            Blog blog = service.DeleteBlog(id);
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            var response = new ServiceResponse<BlogResponse>();
            response.Data = blogResponse;
            response.Message = "Delete Blog";
            response.Status = 200;
            return response;
        }

        [HttpGet("Search")]
        public ActionResult<ServiceResponse<List<BlogResponse>>> SearchBlogByTitle(string title)
        {
            var response = new ServiceResponse<List<BlogResponse>>();
            var blogResponseList = new List<BlogResponse>();
            var blogList = service.GetBlogsByTitle(title);
            foreach (var movie in blogList)
            {
                BlogResponse blogResponse = _mapper.Map<BlogResponse>(movie);
                blogResponseList.Add(blogResponse);
            }

            response.Data = blogResponseList;
            response.Message = "Search Blog By Title";
            response.Status = 200;
            response.TotalDataList = blogResponseList.Count;
            return response;
        }
    }
}
