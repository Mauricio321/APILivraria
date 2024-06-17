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
public class UserController : ControllerBase
{
    const int RoleIdUser = 2;

    private readonly IUserRepositorie userRepositorie;

    public UserController(IUserRepositorie userRepositorie)
    {
        this.userRepositorie = userRepositorie;
    }

    [HttpPost]
    public IActionResult Add(UserViewModel userView)
    {
        var user = new User
        {
            Email = userView.Email,
            Password = userView.Password,
            RoleId = RoleIdUser,
            Carrinho = new Carrinho { }
        };

        string Passwordpattern = @"[@#%&$]";
        var containsSpecialChar = Regex.IsMatch(userView.Password, Passwordpattern);

        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var containsemailPattern = Regex.IsMatch(userView.Email, emailPattern);

        var emailExistente = userRepositorie.EmailExistente(userView.Email);

        if (emailExistente)
        {
            return StatusCode(403, "Esse email ja foi cadastrado");
        }

        if (!containsemailPattern)
        {
            return BadRequest("email invalido, tente novamente");
        }

        if (userView.Password.Length < 8)
        {
            return BadRequest("A senha deve ter pelo menos 8 caracteres");
        }
       
        if (!containsSpecialChar)
        {
            return BadRequest("A senha deve conter caracteres especiais");
        }

        userRepositorie.AddUser(user);

        return Ok();
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
    public IActionResult DeleteUser()
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
}
