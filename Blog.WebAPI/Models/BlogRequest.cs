namespace Blog.WebAPI.Models
{
    public class Blog
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public string BlogContent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
