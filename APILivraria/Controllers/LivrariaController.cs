using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static APILivraria.Services.LivrariaService;

namespace APILivraria.Controllers;

[Route("api/[controller]")]
[ApiController]

[Authorize]
public class LivrariaController : ControllerBase
{
    private readonly ILivrariaRepositorie _livrariaRepositorie;

    private readonly IGenerosRepositories generosRepositories;
    private readonly ILivrariaService livrariaService;

    public LivrariaController(IGenerosRepositories generosRepositories, ILivrariaService livrariaService)
    {
        this.generosRepositories = generosRepositories;
        this.livrariaService = livrariaService;
    }

    [HttpPost]
    [Authorize(Roles = "manager")]
    public async Task<ActionResult<string>> AdicionarLivro(LivrariaSemId DTO, CancellationToken cancellationToken)
    {
        var livroAdicionado = await livrariaService.AdicionarLivro(DTO, cancellationToken);

        return Ok(livroAdicionado);
    }

    [HttpGet("Livros-Disponiveis")]
    [ProducesResponseType(typeof(ListaDeLivros), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public ActionResult<ListaDeLivros> LivrosDisponiveis(int paginas, int quantidadeItensPagina, decimal? precoMinimo, decimal? precoMaximo, [FromQuery] List<int>? genero, OrdenacaoPreco? ordenacaoPreco)
    {
        var cartaDeLivros = livrariaService.ObterTodosLivros(paginas, quantidadeItensPagina, precoMinimo, precoMaximo, genero, ordenacaoPreco, paginas);

        if (!cartaDeLivros.DeuCerto)
        {
            if (cartaDeLivros.TipoDeErro == TiposDeErro.BadRequest)
                return BadRequest(cartaDeLivros.MensagemErro);
        }

        return Ok(cartaDeLivros);
    }

    [HttpDelete("Deletar-Livro")]
    [Authorize(Roles = "manager")]
    public Task<string> DeleteLivro(int id)
    {
        return livrariaService.ApagarLivro(id);
    }
}
