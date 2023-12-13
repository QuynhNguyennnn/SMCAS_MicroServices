namespace BlogService.DTOs.Response
{
    public class AddBlogRequest
    {
        public int UserId { get; set; }

        public string Title { get; set; }

        public string Context { get; set; } = null!;

        public DateTime WritingDate { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool IsDraft { get; set; }
    }
}
