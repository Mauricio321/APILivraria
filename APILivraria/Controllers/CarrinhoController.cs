using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
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

    public CarrinhoController(ILivrariaRepositorie livrariaRepositorie)
    {
        this.livrariaRepositorie = livrariaRepositorie;
    }

    [HttpPost]
    public async Task<ActionResult<string>> AddLivroCarrinho(CarrinhoDto carrinho)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        var id = int.Parse(identity!.FindFirst("userId")!.Value);

        return await livrariaRepositorie.AddCarrinhoItem(carrinho, id, carrinho.LivroId);
    }

    [HttpGet]
    public Task<CarrinhoItemDtoPreco> ReturnLivros()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        var id = int.Parse(identity!.FindFirst("userId")!.Value);

        return livrariaRepositorie.ReturnLivrosCarrinhosByUserId(id);
    }

    [HttpDelete]
    public async Task<string> DeletarLivrosCarrinho(int livroQuantidade, int livroId, bool apagarTodos)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        var id = int.Parse(identity!.FindFirst("userId")!.Value);

        return await livrariaRepositorie.DeleteLivroCarrinho(livroQuantidade, id, livroId, apagarTodos); 
    }
}
