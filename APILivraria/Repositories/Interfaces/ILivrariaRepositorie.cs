using APILivraria.DTOs;
using APILivraria.Models;

namespace APILivraria.Repositories.Interfaces;

public interface ILivrariaRepositorie
{
    Task<string> AdicionarLivro(Livro livraria, CancellationToken cancellationToken);

    ListaDeLivros ObterTodosLivros(int paginas, int quantidadeItensPagina, decimal? precoMinimo, decimal? precoMaximo, List<int>? genero, OrdenacaoPreco? ordenacaoPreco);
    IEnumerable<LivrariaDTO> FiltrarPorNome(string nome);
    void ApagarLivro(string nome);
    Task<string> AddCarrinhoItem(CarrinhoDto carrinho, int userid, int livroid);
    Task<CarrinhoItemDtoPreco> ReturnLivrosCarrinhosByUserId(int Userid);
    decimal LivroCarrinhoJaAdicionadoAntesReturnPreco(IEnumerable<int> livroId);
    Task<string> DeleteLivroCarrinho(int livroQuantidade, int userId, int livroId, bool apagarTodos);
    Task<string> FinalizarCompraCarrinho(int userId, bool apagarTodos);
}


