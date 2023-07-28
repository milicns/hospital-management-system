
using HospitalLibrary.Blog.Dto;
using HospitalLibrary.Blog.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/blog")]
[ApiController]
public class BlogController: ControllerBase
{
    private readonly IBlogService _blogService;
    
    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }
    
    
    [HttpGet("all")]
    [AllowAnonymous]
    public ActionResult GetAllBlogs()
    {
        return Ok(_blogService.GetAllBlogs());
    }
    
    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public ActionResult WriteBlog(CreateBlogDto createBlogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_blogService.WriteBlog(createBlogDto));
    }
    
}