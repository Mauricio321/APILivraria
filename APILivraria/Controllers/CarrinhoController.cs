using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APILivraria.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CarrinhoController : ControllerBase
{
    private readonly ILivrariaRepositorie livrariaRepositorie;
    private readonly ILivrariaService livrariaService;

    public CarrinhoController(ILivrariaRepositorie livrariaRepositorie, ILivrariaService livrariaService)
    {
        this.livrariaRepositorie = livrariaRepositorie;
        this.livrariaService = livrariaService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> AddLivroCarrinho(CarrinhoDto carrinho)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = int.Parse(identity!.FindFirst("userId")!.Value);
        return await livrariaService.AddCarrinhoItem(carrinho, id, carrinho.LivroId);
    }

    [HttpGet]
    public Task<CarrinhoItemDtoPreco> ReturnLivros()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = int.Parse(identity!.FindFirst("userId")!.Value);
        return livrariaService.ReturnLivrosCarrinhosByUserId(id);
    }

    [HttpDelete]
    public async Task<string> DeletarLivrosCarrinho(int livroQuantidade, int livroId)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = int.Parse(identity!.FindFirst("userId")!.Value);
        return await livrariaService.DeleteLivroCarrinho(livroQuantidade, id, livroId); 
    }
}
