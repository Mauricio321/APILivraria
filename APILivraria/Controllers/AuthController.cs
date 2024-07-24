using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;
using APILivraria.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace APILivraria.Controllers;

[ApiController]
[Route("api/v1/Login")]
public class AuthController : Controller
{
    private readonly IUserRepositorie userRepositorie;
    private readonly IUserService userService;

    public AuthController(IUserRepositorie userRepositorie, IUserService userService)
    {
        this.userRepositorie = userRepositorie;
        this.userService = userService;
    }

    [HttpPost]
    public IActionResult AuthUser([FromBody] UserViewModel userView)
    {
        var token = userService.AuthUser(userView.Email, userView.Password);
        
        if(!token.DeuCerto && token.TipoDeErro == TiposDeErro.BadRequest)
        {
            return BadRequest(token.MensagemErro);
        }

        //TiposDeErro tiposDeErro = default;



        return Ok(token.Conteudo);
    }
}
