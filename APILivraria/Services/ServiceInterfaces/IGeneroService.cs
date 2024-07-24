using APILivraria.DTOs;
using APILivraria.Models;

namespace APILivraria.Services.ServiceInterfaces
{
    public interface IGeneroService
    {
        Task<String> AdicionarGenero(GeneroId generos);
        IEnumerable<GeneroDto> GenerosDisponiveis();
    }
}
