using Microsoft.AspNetCore.Mvc;
using Dapper;
using API.Models;
using API.Repositories;
using System.Web.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;

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

    // GET Lawyer/{id}
    /// <summary>
    /// Retorna um advogado.
    /// </summary>
    /// <remarks>
    /// id : id do advogado no banco de dados
    /// </remarks> 
    [HttpGet("{id}", Name = "GetLawyer")]
    [Authorize(Roles = "admin,user")]
    public Lawyer Get(int id)
    {
        Lawyer lawyer = new Lawyer();
        lawyer = lawyerRepository.FindByID(id);
        if (lawyer == null){
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }
        return lawyer;
    }

    // GET Lawyer/{orderBy}/{order}
    /// <summary>
    /// Retorna uma lista de todos os advogados presentes no banco de dados.
    /// </summary>
    /// <remarks>
    /// orderBy : qual parâmetro da tabela que será usada para a ordenação dos registros (Exemplo: "Name" retornará uma lista de advogados ordenados pelo nome).
    /// order : como a lista será ordenada ("ASC" para retornar em ordem ascendente, "DESC" para retornar em ordem descendente)
    /// </remarks> 
    [HttpGet("{orderBy}/{order}",Name = "ListLawyer")]
    [Authorize(Roles = "admin,user")]
    public IEnumerable<Lawyer> List(string orderBy, string order)
    {
        List<Lawyer> lawyerList = new List<Lawyer>();
        IEnumerable<Lawyer> IlawyerList = lawyerList;
        IlawyerList = lawyerRepository.FindAll(orderBy,order);
        return IlawyerList;
    }

    // GET Lawyer/recent/{n}
    /// <summary>
    /// Retorna uma lista de n advogados. Essa lista mostra os advogados criados recentemente primeiro.
    /// </summary>
    /// <remarks>
    /// n : Número que define quantos advogados serão retornados pela API.
    /// </remarks> 
    [HttpGet("recent/{n}",Name = "ListRecentLawyer")]
    [Authorize(Roles = "admin,user")]
    public IEnumerable<Lawyer> ListRecent(int n)
    {
        List<Lawyer> lawyerList = new List<Lawyer>();
        IEnumerable<Lawyer> IlawyerList = lawyerList;
        IlawyerList = lawyerRepository.FindRecentLawyersCreated(n);
        return IlawyerList;
    }

    // POST Lawyer/create
    /// <summary>
    /// Cria um novo advogado no sistema.
    /// </summary>
    /// <remarks>
    /// Exemplo de Body:
    ///
    ///     POST Lawyer/create
    ///     {
    ///        "Name": "Carlos",
    ///        "Email": "carlos@teste.com"
    ///     }
    ///
    /// </remarks>    
    [HttpPost("create", Name = "PostLawyer")]
    [Authorize(Roles = "admin")]
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

    // POST Lawyer/create
    /// <summary>
    /// Edita um advogado já presente no sistema.
    /// </summary>
    /// <remarks>
    /// id : id do advogado no banco de dados
    /// Exemplo de body:
    ///
    ///     POST Lawyer/create
    ///     {
    ///        "Name": "Carlos",
    ///        "Email": "carlos@teste.com"
    ///     }
    ///
    /// </remarks>    
    [HttpPut("edit/{id}", Name = "EditLawyer")]
    [Authorize(Roles = "admin")]
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

    // DELETE Lawyer/remove/{id}
    /// <summary>
    /// Remove um advogado do banco de dados.
    /// </summary>
    /// <remarks>
    /// id : id do advogado no banco de dados
    /// </remarks> 
    [HttpDelete("remove/{id}", Name = "DeleteLawyer")]
    [Authorize(Roles = "admin")]
    public void Delete(int id)
    {
        if (lawyerRepository.FindByID(id) != null){
            lawyerRepository.Remove(id);
        }else{
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }
    }
}


