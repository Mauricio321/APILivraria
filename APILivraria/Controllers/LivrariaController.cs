using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.NovaPasta2;
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
        //var generos = await generosRepositories.ObterGenero(DTO.GeneroIds, cancellationToken);

        //var generosNaoEncontrados = DTO.GeneroIds.Where(generoId => !generos.Any(genero => genero.Id == generoId));

        //if (generosNaoEncontrados.Any())
        //{
        //    return NotFound($"Os seguintes generos nao foram encontrados: {string.Join(", ", generosNaoEncontrados)}");
        //}

        //var generoNovo = generos.Select(genero =>
        //    new LivroGenero
        //    {
        //        Genero = genero
        //    });                                                                                                                             //ISSO TUDO PARA A CAMADA SERVICES

        //var novoLivro = new Livro
        //{
        //    Nome = DTO.Livro,
        //    Generos = generoNovo.ToList(),
        //    Autor = DTO.Autor,
        //    Preco = DTO.Preco,
        //    Quantidade = DTO.Quantidade
        //};

        var livroAdicionado = await _livrariaRepositorie.AdicionarLivro(novoLivro, cancellationToken);

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
