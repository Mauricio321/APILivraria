using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services;
using APILivraria.Services.ServiceInterfaces;
using APILivraria.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace APILivraria.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "manager")]
public class ManagerController : ControllerBase
{
    private readonly IUserRepositorie userRepositorie;
    private readonly IUserService userService;

    public ManagerController(IUserRepositorie managerRepositorie, IUserService userService)
    {
        this.userRepositorie = managerRepositorie;
        this.userService = userService;
    }

    [HttpPost]
    public IActionResult AddManager(ManagerViewModel managerView)
    {
        var envelopUser = userService.AddManager(managerView);

        if (!envelopUser.DeuCerto)
        {
            if (envelopUser.TipoDeErro == TiposDeErro.BadRequest)
            {
                return BadRequest(envelopUser.MensagemErro);
            }

            if (envelopUser.TipoDeErro == TiposDeErro.Forbidden)
            {
                return StatusCode(StatusCodes.Status403Forbidden, envelopUser.MensagemErro);
            }
        }

        return Ok(envelopUser.Conteudo);
    }

    [HttpDelete("Delete-Me")]
    public void Delete()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        var id = int.Parse(identity!.FindFirst("userId")!.Value);

        userService.DeleteUser(id);
    }

    [HttpDelete("Delete-Users")]
    public void DeleteUsers(int Id)
    {
        userService.DeleteOtherUser(Id);
    }
}
