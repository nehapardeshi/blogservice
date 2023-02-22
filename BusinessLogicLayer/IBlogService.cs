using BusinessLogicLayer.Models;

namespace BusinessLogicLayer
{
    public interface IBlogService
    {
        // Add new blog
        Task<Blog> AddBlogAsync(string title, int userId, string blogContent);

        //Update the blog
        Task<bool> UpdateBlogAsync(int blogId, string title, int userId, string blogContent);
        //Get all blogs of an user:
        List<Blog> GetBlogs(int userId);

        //Get one blog:
        Blog? GetBlog(int blogId);

        // Delete blog:
        Task<bool> DeleteBlogAsync(int blogId);

        // Search blog with text:
        List<Blog> SearchBlogs(string searchText);
        void GetBlogs(object blogId, object title, object userId, object blogContent);
    }
}
