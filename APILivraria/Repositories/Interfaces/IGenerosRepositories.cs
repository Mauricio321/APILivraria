using APILivraria.Models;

namespace APILivraria.Repositories.Interfaces
{
    public interface IGenerosRepositories
    {
        Task<String> AdicionarGenero(Generos generos);
        Task<List<Generos>> ObterGenero(List<int> id, CancellationToken cancellationToken);
        IEnumerable<Generos> GenerosDisponiveis();
        void ApagarGenero(int id);
    }
}
