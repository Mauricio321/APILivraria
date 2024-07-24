using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;

namespace APILivraria.Services;

public class GeneroService : IGeneroService
{
    private readonly IGenerosRepositories generosRepositories;
    public GeneroService(IGenerosRepositories generosRepositories)
    {
        this.generosRepositories = generosRepositories;
    }

    public Task<string> AdicionarGenero(GeneroId generos)
    {
        var genero = new Generos
        {
            Nome = generos.Nome,
        };
        return generosRepositories.AdicionarGenero(genero);
    }

    public IEnumerable<GeneroDto> GenerosDisponiveis()
    {
        var generosDisponiveis = generosRepositories.GenerosDisponiveis();

        var generos = generosDisponiveis.Select(g => new GeneroDto { Nome = g.Nome, id = g.Id });

        return generos;
    }
}
