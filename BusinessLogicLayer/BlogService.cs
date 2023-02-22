using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _context;

        public BlogService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<Blog> AddBlogAsync(string title, int userId, string blogContent)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }
            else
            {
                // Create new blog:

                var blog = new Blog
                {
                    UserId = userId,
                    Title = title,
                    Content = blogContent,
                    CreatedDate = DateTime.Now
                };
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();
                return blog;
            }
        }

        public async Task<bool> DeleteBlogAsync(int blogId)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.BlogId == blogId);

            if (blog == null)
            {
                throw new BlogIdNotFoundException(blogId);
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<Blog> GetBlogs(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
            {
                throw new UserNotFoundException
                    ($"User not found with this Id {userId}!");
            }
            else
            {
                return _context.Blogs.Where(b => b.UserId == userId).ToList();
            }
        }

        public Blog? GetBlog(int blogId) => _context?.Blogs?.FirstOrDefault(b => b.BlogId == blogId);

        public List<Blog> SearchBlogs(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return new List<Blog>();

            var searchedBlog = _context.Blogs.Where(b =>
                                                    b.Title.Contains(searchText) ||
                                                    b.Content.Contains(searchText));

            return searchedBlog.ToList();
        }

        public async Task<bool> UpdateBlogAsync(int blogId, string title, int userId, string blogContent)
        {

            //First check userId:
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);

            }

            // Update the blog content or title:
            var blog = _context.Blogs.FirstOrDefault(b => b.BlogId == blogId);
            if (blog == null)
            {
                throw new BlogIdNotFoundException(blogId);
            }
            blog.Title = title;
            blog.Content = blogContent;
            await _context.SaveChangesAsync();
            return true;
        }

        public void GetBlogs(object blogId, object title, object userId, object blogContent)
        {
            throw new NotImplementedException();
        }
    }
}
