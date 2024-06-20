using APILivraria.DTOs;
using APILivraria.Models;
using static APILivraria.Services.LivrariaService;

namespace APILivraria.Services.ServiceInterfaces
{
    public interface ILivrariaService
    {
        Task<string> AdicionarLivro(Livro livraria, CancellationToken cancellationToken);
        Envelope<ListaDeLivros> ObterTodosLivros(int paginaAtual, int quantidadeItensPagina, decimal? precoEntreMin, decimal? precoEntreMax, List<int>? generoIds, OrdenacaoPreco? ordenacaoPreco, int pagina)
        Task<string> ApagarLivro(int id);
        Task<string> AddCarrinhoItem(CarrinhoDto carrinho, int userId, int livroId);
        Task<CarrinhoItemDtoPreco> ReturnLivrosCarrinhosByUserId(int userId);
        Task<string> DeleteLivroCarrinho(int livroQuantidade, int userId, int livroId);
        Task<string> FinalizarCompraCarrinho(int userId, bool apagarTodos);
    }
}

