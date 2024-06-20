using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;

namespace APILivraria.Services
{
    public class LivrariaService : ILivrariaService
    {
        private readonly ILivrariaRepositorie livrariaRepositorie;
        private readonly IUserRepositorie userRepositorie;

        public LivrariaService(ILivrariaRepositorie livrariaRepositorie, IUserRepositorie userRepositorie)
        {
            this.livrariaRepositorie = livrariaRepositorie;
            this.userRepositorie = userRepositorie;
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

        public Envelope<ListaDeLivros> ObterTodosLivros(int paginaAtual, int quantidadeItensPagina, decimal? precoEntreMin, decimal? precoEntreMax, List<int>? generoIds, OrdenacaoPreco? ordenacaoPreco, int pagina)
        {
            if (quantidadeItensPagina <= 0)
            {
                return new Envelope<ListaDeLivros> { DeuCerto = false, MensagemErro = "Interessante escolha de quantidade de itens. Itens começam a partir do número 1, mas parece que você quer ser diferente. Que tal tentar um número positivo desta vez?", Conteudo = null, TipoDeErro = TiposDeErro.BadRequest };
            }
            if (pagina <= 0)
            {
                return new Envelope<ListaDeLivros> { DeuCerto = false, MensagemErro = "Se você realmente acha que existe uma página menor ou igual a zero, talvez precise de um novo conceito de matemática.Tente de novo.", TipoDeErro = TiposDeErro.BadRequest };
            }

            var listaDeLivros = livrariaRepositorie.ObterTodosLivros(paginaAtual, quantidadeItensPagina, precoEntreMin, precoEntreMax, generoIds, ordenacaoPreco);

            return new Envelope<ListaDeLivros> { DeuCerto = true, Conteudo = listaDeLivros };
        }

        public async Task<string> AddCarrinhoItem(CarrinhoDto carrinho, int userId, int livroId)
        {
            var usuarioExistente = await userRepositorie.GetUserById(userId);

            if (usuarioExistente == null)
            {
                return "usuario nao encontrado";
            }

            var livroJaAdicionadoAntes = usuarioExistente.Carrinho.CarrinhoItens.FirstOrDefault(item => item.LivroId == livroId);

            if (livroJaAdicionadoAntes != null)
            {
                livroJaAdicionadoAntes.Quantidade += carrinho.QuantidadeItens;

                await livrariaRepositorie.CarrinhoItemUpdate(livroJaAdicionadoAntes);
            }

            var carrinhoItem = new CarrinhoItem();

            var addCarrinhoItem = livrariaRepositorie.AddCarrinhoItem(carrinhoItem);

            return "livro adocionado no carrinho";
        }

        public async Task<CarrinhoItemDtoPreco> ReturnLivrosCarrinhosByUserId(int userId)
        {

            var user = await userRepositorie.GetUserById(userId);

            if (user == null)
            {
                return null;
            }

            var carrinhoitens = user.Carrinho.CarrinhoItens;
            var carrinhoItem = new List<CarrinhoItemDto>();
            var itemPreco = decimal.Zero;

            foreach (var item in carrinhoitens)
            {
                var livros = item.Livro;
                var quantidade = item.Quantidade;

                var carrinhoItens = new CarrinhoItemDto
                {
                    Livro = livros.Nome,
                    Quantidade = quantidade,
                    Preco = livros.Preco,
                };
                itemPreco += livros.Preco * quantidade;
                carrinhoItem.Add(carrinhoItens);
            }

            var carrinhoItemPreco = new CarrinhoItemDtoPreco
            {
                CarrinhoItemsDto = carrinhoItem,
                PrecoTotal = itemPreco,
                QuantidadeTotal = user.Carrinho.QuantidadeItens
            };

            return carrinhoItemPreco;
        }

        public async Task<string> DeleteLivroCarrinho(int livroQuantidade, int userId, int livroId)
        {
            var user = await livrariaRepositorie.FinalizarCompraCarrinho(userId);
            var carrinho = user.Carrinho;
            var itemToRemove = user.Carrinho.CarrinhoItens
                                            .FirstOrDefault(l => l.Livro.Id == livroId);

            if (user == null || user.Carrinho == null)
            {
                return "usuario nao encontrado";
            }

            if (itemToRemove?.Quantidade >= livroQuantidade)
            {
                itemToRemove.Quantidade -= livroQuantidade;
                livrariaRepositorie.UpdateCarrinhoItem(itemToRemove);
                livrariaRepositorie.SaveChangesAsync();
            }

            else
            {
                livrariaRepositorie.RemoveCarrinhoItens(carrinho.CarrinhoItens);
                carrinho.CarrinhoItens.Clear();
                livrariaRepositorie.SaveChangesAsync();
            }

            return "livro removida da lista";
        }

        public async Task<string> FinalizarCompraCarrinho(int userId, bool finalizarCompra)
        {
            var user = await livrariaRepositorie.FinalizarCompraCarrinho(userId);

            if (user == null || user.Carrinho == null)
            {
                return "usuario ou carrinho nao encontrado";
            }

            var carrinho = user.Carrinho;

            if (finalizarCompra)
            {
                livrariaRepositorie.RemoveCarrinhoItens(carrinho.CarrinhoItens);
                carrinho.CarrinhoItens.Clear();
            }

            return "Compra finalizada";
        }
    }
}

