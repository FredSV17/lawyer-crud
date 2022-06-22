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

    [HttpGet("{id}", Name = "GetLawyer")]
    public Lawyer Get(int id)
    {
        Lawyer lawyer = new Lawyer();
        lawyer = lawyerRepository.FindByID(id);
        return lawyer;
    }

    [HttpGet("", Name = "ListLawyer")]
    public IEnumerable<Lawyer> List()
    {
        List<Lawyer> lawyerList = new List<Lawyer>();
        IEnumerable<Lawyer> IlawyerList = lawyerList;
        IlawyerList = lawyerRepository.FindAll();
        return IlawyerList;
    }

    [HttpPost("create", Name = "PostLawyer")]
    public string Create(LawyerDTO lawyerDTO)
    {
        Lawyer lawyer = new Lawyer(lawyerDTO.Name,lawyerDTO.Email);
        lawyerRepository.Add(lawyer);
        return "create";
    }
    [HttpPut("edit/{id}", Name = "EditLawyer")]
    public string Edit(int id,LawyerDTO lawyerDTO)
    {
        Lawyer lawyer = new Lawyer(id,lawyerDTO.Name,lawyerDTO.Email);
        lawyerRepository.Update(lawyer);
        return "update";

    }
    //TODO: Alterar Get's
    [HttpDelete("remove/{id}", Name = "DeleteLawyer")]
    public string Delete(int id)
    {
        lawyerRepository.Remove(id);
        return "delete";
    }
}


