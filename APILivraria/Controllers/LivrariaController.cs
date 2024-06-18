using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.NovaPasta2;
using APILivraria.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var generos = DTO.GeneroId.Count != 0 ? await generosRepositories.ObterGeneros(DTO.GeneroId, cancellationToken) : new Dictionary<int, Generos?>();

        var generosNaoEncontrados = generos.Where(kvp => kvp.Value is null);

        if (generosNaoEncontrados.Any())
        {
            return NotFound($"Os seguintes generos nao foram encontraqdos: {string.Join(", ", generosNaoEncontrados.Select(kvp => kvp.Key.ToString()))}");
        }

        var generoNovo = new List<LivroGenero>();
        foreach (var genero in generos)
        {
            generoNovo.Add(new LivroGenero
            {
                Genero = genero.Value!
            });
        }

        var compraLivros = new Livro
        {
            Nome = DTO.Livro,
            Generos = generoNovo,
            Autor = DTO.Autor,
            Preco = DTO.Preco,
            Quantidade = DTO.Quantidade
        };

        var livroAdicionado = await _livrariaRepositorie.AdicionarLivro(compraLivros, cancellationToken);

        return Ok(livroAdicionado);
    }

    [HttpGet("Livros-Disponiveis")]
    [ProducesResponseType(typeof(ListaDeLivros), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public ActionResult<ListaDeLivros> LivrosDisponiveis(int paginas, int quantidadeItensPagina, decimal? precoMinimo, decimal? precoMaximo, [FromQuery] List<int>? genero, OrdenacaoPreco? ordenacaoPreco)
    {
        if (paginas <= 0)
        {
            return BadRequest("Se você realmente acha que existe uma página menor ou igual a zero, talvez precise de um novo conceito de matemática. Tente de novo.");
        }

        if (quantidadeItensPagina <= 0)
        {
            return BadRequest("Interessante escolha de quantidade de itens. Itens começam a partir do número 1, mas parece que você quer ser diferente. Que tal tentar um número positivo desta vez?");
        }

        var livros = _livrariaRepositorie.ObterTodosLivros(paginas, quantidadeItensPagina, precoMinimo, precoMaximo, genero, ordenacaoPreco);

        return Ok(livros);
    }

    [HttpDelete("Deletar-Livro")]
    [Authorize(Roles = "manager")]
    public Task<string> DeleteLivro(int id)
    {
        return livrariaService.ApagarLivro(id);
    }
}
