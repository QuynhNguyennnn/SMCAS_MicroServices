namespace BlogService.DTOs.Response
{
    public class UpdateBlogResquest
    {
        public int BlogId { get; set; }

        public string Context { get; set; } = null!;

        public bool IsDraft { get; set; }
    }
}
