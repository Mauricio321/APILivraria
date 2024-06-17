using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
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

    public ManagerController(IUserRepositorie managerRepositorie)
    {
        this.userRepositorie = managerRepositorie;
    }

    [HttpPost]
    public IActionResult AddManager(ManagerViewModel managerView)
    {
        const int RoleIdManager = 1;

        var manager = new User()
        {
            Email = managerView.Email,
            Password = managerView.Password,
            RoleId = RoleIdManager
        };

        string Passwordpattern = @"[@#%&$]";
        var containsSpecialChar = Regex.IsMatch(managerView.Password, Passwordpattern);

        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var containsemailPattern = Regex.IsMatch(managerView.Email, emailPattern);

        var emailExistente = userRepositorie.EmailExistente(managerView.Email);

        if (emailExistente)
        {
            return StatusCode(403, "Esse email ja foi cadastrado");
        }
       
        if (!containsemailPattern)
        {
            return BadRequest("email invalido, tente novamente");
        }

        if (managerView.Password.Length < 8)
        {
            return BadRequest("A senha deve ter pelo menos 8 caracteres");
        }
       
        if (!containsSpecialChar)
        {
            return BadRequest("A senha deve conter caracteres especiais");
        }

        userRepositorie.AddUser(manager);
        return Ok();
    }

    [HttpDelete("Delete-Me")]
    public IActionResult Delete()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = int.Parse(identity!.FindFirst("userId")!.Value);

        var idExistente = userRepositorie.IdExistente(id);

        if (idExistente)
        {
            return BadRequest("Usuario não encontrado");
        }

        userRepositorie.DeleteUser(id);

        return Ok("Usuario deletada com sucesso");
    }

    [HttpDelete("Delete-Users")]
    public IActionResult DeleteUsers(int Id)
    {

        var usuarioExistente = userRepositorie.IdExistente(Id);
        if (!usuarioExistente)
        {
            return NotFound("usuario não encontrado");
        }

        userRepositorie.DeleteOtherUsers(Id);
        return Ok("usuario deletado");
    }
}
