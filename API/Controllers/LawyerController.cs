using Microsoft.AspNetCore.Mvc;
using Dapper;
using API.Models;
using API.Repositories;
using System.Web.Http;
using System.Net;

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
        if (lawyer == null){
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }
        return lawyer;
    }

    [HttpGet(Name = "ListLawyer")]
    public IEnumerable<Lawyer> List()
    {
        List<Lawyer> lawyerList = new List<Lawyer>();
        IEnumerable<Lawyer> IlawyerList = lawyerList;
        IlawyerList = lawyerRepository.FindAll();
        return IlawyerList;
    }

    [HttpPost("create", Name = "PostLawyer")]
    public void Create(LawyerDTO lawyerDTO)
    {
        Lawyer lawyer = new Lawyer(lawyerDTO.Name,lawyerDTO.Email);
        if (lawyerRepository.FindByEmail(lawyerDTO.Email) == null){
            lawyerRepository.Add(lawyer);
        }else{
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }
    }
    [HttpPut("edit/{id}", Name = "EditLawyer")]
    public void Edit(int id,LawyerDTO lawyerDTO)
    {
        if (lawyerRepository.FindByID(id) != null){
            Lawyer lawyer = new Lawyer(id,lawyerDTO.Name,lawyerDTO.Email);
            lawyerRepository.Update(lawyer);
        }else{
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }

    }

    [HttpDelete("remove/{id}", Name = "DeleteLawyer")]
    public void Delete(int id)
    {
        if (lawyerRepository.FindByID(id) != null){
            lawyerRepository.Remove(id);
        }else{
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }
    }
}


