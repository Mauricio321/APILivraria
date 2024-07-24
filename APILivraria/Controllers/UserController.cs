using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;
using APILivraria.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace APILivraria.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepositorie userRepositorie;
    private readonly IUserService userService;

    public UserController(IUserRepositorie userRepositorie, IUserService userService)
    {
        this.userRepositorie = userRepositorie;
        this.userService = userService;
    }

    [HttpPost]
    public IActionResult Add(UserViewModel userView)
    {
        var envelopUser = userService.AddUser(userView);

        if (!envelopUser.DeuCerto)
        {
            if (envelopUser.TipoDeErro == TiposDeErro.BadRequest)
            {
                return BadRequest(envelopUser.MensagemErro);
            }

            if(envelopUser.TipoDeErro == TiposDeErro.Forbidden)
            {
                return StatusCode(StatusCodes.Status403Forbidden, envelopUser.MensagemErro);
            }
        }

        return Ok(envelopUser.Conteudo);
    }

    [Authorize(Roles = "manager")]
    [HttpGet]
    public IActionResult ReturnUser()
    {
        var users = userRepositorie.Get();

        return Ok(users);
    }

    [Authorize(Roles = "manager")]
    [HttpDelete]
    public void DeleteUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        var id = int.Parse(identity!.FindFirst("userId")!.Value);

        userService.DeleteUser(id);
    }
}
