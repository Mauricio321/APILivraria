using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APILivraria.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "manager")]
[ApiController]
public class GenerosController : ControllerBase
{
    private readonly IGenerosRepositories generosRepositories;

    public GenerosController(IGenerosRepositories generosRepositories)
    {
        this.generosRepositories = generosRepositories;
    }

    [HttpPost]
    public async Task<ActionResult<string>> AdicionarGenero(GeneroId generos)
    {
        var GenerosLivro = new Generos
        {
            Nome = generos.Nome
        };

        var GeneroAdd = await generosRepositories.AdicionarGenero(GenerosLivro);

        return Ok(GeneroAdd);
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetGeneros()
    {
        var generosDisponiveis = generosRepositories.GenerosDisponiveis();

        return Ok(generosDisponiveis);
    }

    [HttpDelete]
    public IActionResult DeleteGenero(int id)
    {
        generosRepositories.ApagarGenero(id);

        return Ok();
    }
}
