using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;
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
    private readonly IGeneroService generoService;

    public GenerosController(IGenerosRepositories generosRepositories, IGeneroService generoService)
    {
        this.generosRepositories = generosRepositories;
        this.generoService = generoService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> AdicionarGenero(GeneroId generos)
    {
        var GeneroAdd = await generoService.AdicionarGenero(generos);
        return Ok(GeneroAdd);
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetGeneros()
    {
        var generosDisponiveis = generoService.GenerosDisponiveis();
        return Ok(generosDisponiveis);
    }

    [HttpDelete]
    public IActionResult DeleteGenero(int id)
    {
        generosRepositories.ApagarGenero(id);
        return Ok();
    }
}
