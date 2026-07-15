namespace MyFirstBlog.Controllers;

using Microsoft.AspNetCore.Mvc;
using MyFirstBlog.Dtos;
using MyFirstBlog.Services;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    // GET /posts
    [HttpGet]
    public IEnumerable<PostDto> GetPosts()
    {
        return _postService.GetPosts();
    }

    // GET /posts/{slug}
    [HttpGet("{slug}")]
    public ActionResult<PostDto> GetPost(string slug)
    {
        var post = _postService.GetPost(slug);

        if (post is null)
        {
            return NotFound();
        }

        return post;
    }

    // POST /posts
    [HttpPost]
    public ActionResult<object> CreatePost([FromBody] CreatePostDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest(new
            {
                errors = new[] { "Title cannot be blank" }
            });
        }

        var post = _postService.CreatePost(request);

        return Created($"/posts/{post.Slug}", new
        {
            post = new
            {
                title = post.Title,
                description = post.Body
            }
        });
    }
}