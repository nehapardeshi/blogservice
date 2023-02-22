using BusinessLogicLayer;
using BusinessLogicLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;


        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> AddBlog([FromBody] Models.Blog blog)
        {
            try
            {
                var createdBlog = await _blogService.AddBlogAsync(blog.Title, blog.UserId, blog.BlogContent);
                return Ok(createdBlog.BlogId);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Blog))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetBlog(int id)
        {
            var blog = _blogService.GetBlog(id);

            if (blog == null)
                return NotFound($"Blog not found with Id {id}");

            var blogModel = new Models.Blog
            {
                BlogId = id,
                UserId = blog.UserId,
                Title = blog.Title,
                BlogContent = blog.Content,
                CreatedDate = blog.CreatedDate
            };
            return Ok(blogModel);
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Models.Blog>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlogs(int userId)
        {
            List<Models.Blog> blogs = new List<Models.Blog>();
            try
            {
                var userBlogs = _blogService.GetBlogs(userId);
                foreach (var blog in userBlogs)
                {
                    blogs.Add(new Models.Blog
                    {
                        BlogId = blog.BlogId,
                        CreatedDate = blog.CreatedDate,
                        Title = blog.Title,
                        UserId = userId
                    });

                }
                return Ok(blogs);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }
        }
        [HttpGet]
        [Route("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Models.Blog>))]
        public async Task<IActionResult> SearchBlogs(string searchText)
        {
            List<Models.Blog> foundBlogs = new List<Models.Blog>();
            try
            {
                var blogs = _blogService.SearchBlogs(searchText);
                foreach (var blog in blogs)
                {
                    foundBlogs.Add(new Models.Blog
                    {
                        BlogId = blog.BlogId,
                        CreatedDate = blog.CreatedDate,
                        Title = blog.Title,
                        UserId = blog.UserId
                    });

                }
                return Ok(foundBlogs);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBlog([FromBody] WebAPI.Models.Blog blog)
        {
            try
            {
                await _blogService.UpdateBlogAsync(blog.BlogId, blog.Title, blog.UserId, blog.BlogContent);
                return Ok(true);
            }
            catch (UserNotFoundException ex)
            {

                return NotFound(ex.ToString());
            }
            catch (BlogIdNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            try
            {
                await _blogService.DeleteBlogAsync(blogId);
                return Ok(true);
            }
            catch (BlogIdNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }

        }
    }
}
