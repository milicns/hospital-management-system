using IntegrationLibrary.BloodBank.Dto;
using IntegrationLibrary.BloodBank.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAPI.Controllers;


[Route("api/news")]
[ApiController]
public class BloodBankNewsController : ControllerBase
{
    private readonly IBloodBankNewsService _bloodBankNewsService;
    
    public BloodBankNewsController(IBloodBankNewsService bloodBankNewsService)
    {
        _bloodBankNewsService = bloodBankNewsService;
    }
    
    [HttpGet]
    [Authorize(Roles="SystemAdministrator")]
    public ActionResult GetAllReceived()
    {
        return Ok(_bloodBankNewsService.GetAllReceived());
    }
    
    [HttpGet("published")]
    [AllowAnonymous]
    public ActionResult GetAllPublished()
    {
        return Ok(_bloodBankNewsService.GetAllPublished());
    }

    [HttpPut("{id}")]
    [Authorize(Roles="SystemAdministrator")]
    public ActionResult UpdateNews(int id, BloodBankNewsDto newsDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != newsDto.Id)
        {
            return BadRequest();
        }

        try
        {
            _bloodBankNewsService.UpdateNews(newsDto);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(newsDto);
    }
    
    
}