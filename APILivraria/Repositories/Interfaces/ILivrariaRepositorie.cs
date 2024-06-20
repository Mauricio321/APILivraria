using APILivraria.DTOs;
using APILivraria.Models;

namespace APILivraria.Repositories.Interfaces;

public interface ILivrariaRepositorie
{
    Task<string> AdicionarLivro(Livro livraria, CancellationToken cancellationToken);

    ListaDeLivros ObterTodosLivros(int paginas, int quantidadeItensPagina, decimal? precoMinimo, decimal? precoMaximo, List<int>? genero, OrdenacaoPreco? ordenacaoPreco);
    Task<Livro?> FiltrarLivroById(int id);
    void ApagarLivro(Livro livro);
    Task CarrinhoItemUpdate(CarrinhoItem carrinhoItem);
    Task AddCarrinhoItem(CarrinhoItem carrinhoItem);
    Task<User> FinalizarCompraCarrinho(int userId);
    bool LivroCarrinhoJaAdicionadoAntes(int userId, int livroId);
    void RemoveCarrinhoItens(IEnumerable<CarrinhoItem> carrinhoItens);
    void UpdateCarrinhoItem(CarrinhoItem item);
    void SaveChangesAsync();
}


