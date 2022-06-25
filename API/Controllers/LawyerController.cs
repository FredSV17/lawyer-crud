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

    [HttpGet("{orderBy}/{order}",Name = "ListLawyer")]
    public IEnumerable<Lawyer> List(string orderBy, string order)
    {
        List<Lawyer> lawyerList = new List<Lawyer>();
        IEnumerable<Lawyer> IlawyerList = lawyerList;
        IlawyerList = lawyerRepository.FindAll(orderBy,order);
        return IlawyerList;
    }

    [HttpGet("recent/{id}",Name = "ListRecentLawyer")]
    public IEnumerable<Lawyer> ListRecent(int id)
    {
        List<Lawyer> lawyerList = new List<Lawyer>();
        IEnumerable<Lawyer> IlawyerList = lawyerList;
        IlawyerList = lawyerRepository.FindRecentLawyersCreated(id);
        return IlawyerList;
    }

    [HttpPost("create", Name = "PostLawyer")]
    public void Create(LawyerDTO lawyerDTO)
    {
        DateTime createdAt = DateTime.UtcNow;
        Lawyer lawyer = new Lawyer(lawyerDTO.Name,lawyerDTO.Email,createdAt);
        if (lawyerRepository.FindByEmail(lawyerDTO.Email) == null){
            lawyerRepository.Add(lawyer);
        }else{
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }
    }
    [HttpPut("edit/{id}", Name = "EditLawyer")]
    public void Edit(int id,LawyerDTO lawyerDTO)
    {
        Lawyer lawyer = new Lawyer();
        lawyer = lawyerRepository.FindByID(id);
        if (lawyer != null){
            Lawyer editLawyer = new Lawyer(id,lawyerDTO.Name,lawyerDTO.Email,lawyer.CreatedAt);
            lawyerRepository.Update(editLawyer);
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


