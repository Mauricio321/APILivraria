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

        public async Task<string> ApagarLivro(int id)
        {
            var livro = await livrariaRepositorie.FiltrarLivroById(id);

            if (livro == null)
            {
                return "livro nao encontrado";
            }

            livrariaRepositorie.ApagarLivro(livro);
            return "livro deletado";
        }

        public ListaDeLivros ObterTodosLivros(int paginaAtual, int quantidadeItensPagina, decimal? precoEntreMin, decimal? precoEntreMax, List<int>? generoIds, OrdenacaoPreco? ordenacaoPreco)
        {
            return livrariaRepositorie.ObterTodosLivros(paginaAtual, quantidadeItensPagina, precoEntreMin, precoEntreMax, generoIds, ordenacaoPreco);
        }



        public Task<string> AddCarrinhoItem(CarrinhoDto carrinho, int userId, int livroId)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteLivroCarrinho(int livroQuantidade, int userId, int livroId, bool apagarTodos)
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

        public Task<CarrinhoItemDtoPreco> ReturnLivrosCarrinhosByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

