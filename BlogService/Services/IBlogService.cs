using BlogService.Models;

namespace BlogService.Services
{
    public interface IBlogService
    {
        List<Blog> GetBlogList();
        Blog GetBlogById(int id);
        Blog CreateBlog(Blog blog);
        Blog UpdateBlog(Blog blog);
        Blog DeleteBlog(int id);
    }
}
