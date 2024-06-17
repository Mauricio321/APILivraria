using APILivraria.Repositories.Interfaces;
using APILivraria.Services;
using APILivraria.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace APILivraria.Controllers;

[ApiController]
[Route("api/v1/Login")]
public class AuthController : Controller
{
    private readonly IUserRepositorie userRepositorie;

    public AuthController(IUserRepositorie userRepositorie)
    {
        this.userRepositorie = userRepositorie;
    }

    [HttpPost]
    public IActionResult AuthUser([FromBody] UserViewModel userView)
    {
        var user = userRepositorie.GetUser(userView.Email, userView.Password);

        if (user != null)
        {
            var token = TokenServices.GenerateToken(user);
            return Ok(token);
        }
       
        return BadRequest("Username or password is invalid");
    }
}
