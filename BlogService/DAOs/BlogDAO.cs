using BlogService.Models;

namespace BlogService.DAOs
{
    public class BlogDAO
    {
        public static List<Blog> GetBlogList()
        {
            List<Blog> blogs = new List<Blog>();
            try
            {
                using (var context = new SepprojectDbContext())
                {
                    var blogList = context.Blogs.ToList();
                    foreach (var movie in blogList)
                    {
                        if (movie.IsActive)
                        {
                            blogs.Add(movie);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return blogs;
        }

        public static Blog GetBlogById(int id)
        {
            Blog blog = new Blog();
            try
            {
                using (var context = new SepprojectDbContext())
                {
                    blog = context.Blogs.SingleOrDefault(b => (b.BlogId == id) && b.IsActive);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return blog;
        }

        public static Blog CreateBlog(Blog blog)
        {
            try
            {
                using (var context = new SepprojectDbContext())
                {
                    blog.IsActive = true;

                    context.Blogs.Add(blog);
                    context.SaveChanges();

                    return blog;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Blog UpdateBlog(Blog blog)
        {
            try
            {
                using (var context = new SepprojectDbContext())
                {
                    var _blog = GetBlogById(blog.BlogId);
                    if (_blog != null)
                    {
                        blog.IsActive = _blog.IsActive;
                        blog.WritingDate = _blog.WritingDate;
                        blog.PublishedDate = _blog.PublishedDate;
                        blog.UserId = _blog.UserId;

                        // Sử dụng SetValues để cập nhật giá trị từ movie vào _movie
                        context.Entry(_blog).CurrentValues.SetValues(blog);
                        context.SaveChanges();

                        return _blog; // Trả về _movie sau khi cập nhật
                    }
                    else
                    {
                        throw new Exception("Blog does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Blog DeleteBlog(int id)
        {
            try
            {
                using (var context = new SepprojectDbContext())
                {
                    var _blog = context.Blogs.SingleOrDefault(b => b.BlogId == id && b.IsActive);
                    if (_blog != null)
                    {
                        _blog.IsActive = false;

                        // Sử dụng SetValues để cập nhật giá trị từ movie vào _movie
                        context.Entry(_blog).CurrentValues.SetValues(_blog);
                        context.SaveChanges();

                        return _blog; // Trả về _movie sau khi cập nhật
                    }
                    else
                    {
                        throw new Exception("Blog does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
