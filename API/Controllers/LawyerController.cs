using Microsoft.AspNetCore.Mvc;
using Dapper;
using AspNetCoreDapper.Models;
using AspNetCoreDapper.Repositories;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class LawyerController : ControllerBase
{

    private readonly LawyerRepository lawyerRepository;
    private readonly ILogger<LawyerController> _logger;

    public LawyerController(ILogger<LawyerController> logger, IConfiguration configuration)
    {
        _logger = logger;
        lawyerRepository = new LawyerRepository(configuration);
    }

    [HttpGet(Name = "GetLawyer")]
    public string Get()
    {
        return "Get";
    }

    [HttpGet("create", Name = "PostLawyer")]
    public string Create()
    {
        Lawyer lawyer = new Lawyer("José","joseemail.com");
        lawyerRepository.Add(lawyer);
        return "Create";
    }
    [HttpGet("edit", Name = "EditLawyer")]
    public string Edit()
    {
        return "Edit";
    }
    [HttpDelete("delete", Name = "DeleteLawyer")]
    public string Delete()
    {
        return "Delete";
    }
}


