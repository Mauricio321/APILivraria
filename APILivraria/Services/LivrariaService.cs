using APILivraria.Data;
using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories;
using APILivraria.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APILivraria.Services
{
    public class LivrariaService : ILivrariaService
    {
        private readonly ILivrariaRepositorie livrariaRepositorie;

        public LivrariaService(ILivrariaRepositorie livrariaRepositorie)
        {
            this.livrariaRepositorie = livrariaRepositorie;
        }
        public Task<string> AdicionarLivro(Livro livraria, CancellationToken cancellationToken)
        {
            return livrariaRepositorie.AdicionarLivro(livraria, cancellationToken);
        }
        public Task ApagarLivro(string nome)
        {
           
        }

        public Task<string> AddCarrinhoItem(CarrinhoDto carrinho, int userId, int livroId)
        {
            throw new NotImplementedException();
        }



        public Task<string> DeleteLivroCarrinho(int livroQuantidade, int userId, int livroId, bool apagarTodos)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LivrariaDTO> FiltrarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Task<string> FinalizarCompraCarrinho(int userId, bool apagarTodos)
        {
            throw new NotImplementedException();
        }

        public decimal LivroCarrinhoJaAdicionadoAntesReturnPreco(IEnumerable<int> livroId)
        {
            throw new NotImplementedException();
        }

        public ListaDeLivros ObterTodosLivros(int paginas, int quantidadeItensPagina, decimal? precoMinimo, decimal? precoMaximo, List<int>? genero, OrdenacaoPreco? ordenacaoPreco)
        {
            throw new NotImplementedException();
        }

        public Task<CarrinhoItemDtoPreco> ReturnLivrosCarrinhosByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }

     