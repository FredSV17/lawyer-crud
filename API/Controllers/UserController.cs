using Microsoft.AspNetCore.Mvc;
using Dapper;
using API.Models;
using API.Repositories;
using System.Web.Http;
using System.Net;
using API.Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly UserRepository userRepository;
    private readonly ILogger<LawyerController> _logger;

    public UserController(ILogger<LawyerController> logger, IConfiguration configuration)
    {
        _logger = logger;
        userRepository = new UserRepository(configuration);
    }

    // POST User/create
    /// <summary>
    /// Cria um novo usuário no sistema.
    /// </summary>
    /// <remarks>
    /// Exemplo de Body:
    ///
    ///     POST Lawyer/create
    ///     {
    ///        "name": "User",
    ///        "email": "user@teste.com"
    ///        "password": "password"
    ///     }
    ///
    /// </remarks>  
    [HttpPost("create", Name = "CreateUser")]
    public void CreateUser(UserDTO userDTO)
    {
        if (userRepository.FindByEmail(userDTO.email) == null){
            User user = new User(userDTO.name,userDTO.email,userDTO.password,"user");
            userRepository.Add(user);
        }else{
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }
    }

    // GET User/token
    /// <summary>
    /// Gera um token Bearer para um usuário existente no sistema.
    /// </summary>
    [HttpGet("token", Name = "TokenGen")]
    public string TokenGen([FromQuery]TokenGenDTO tokenGenDTO)
    {
        User user = new User();
        user = userRepository.Get(tokenGenDTO.email, tokenGenDTO.password);
        if (user == null){
            throw new HttpResponseException(HttpStatusCode.UnprocessableEntity);
        }

        var token = TokenService.GenerateToken(user);
        return token;
    }

}


