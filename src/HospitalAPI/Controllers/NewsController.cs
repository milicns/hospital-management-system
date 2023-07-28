
using HospitalLibrary.News.Dto;
using HospitalLibrary.News.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[Route("api/news")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly  INewsService _newsService;

    public NewsController(INewsService newsService)
    { 
        _newsService = newsService;
    }
    
    [HttpPost]
    [Authorize(Roles = "SystemAdministrator")]
    public ActionResult PublishNews(PublishNewsDto publishNewsDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_newsService.Create(publishNewsDto));
    }

    [HttpGet("all")]
    public ActionResult GetAllNews()
    {
        return Ok(_newsService.GetAll());
    }
}