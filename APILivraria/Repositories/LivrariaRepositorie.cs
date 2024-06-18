using APILivraria.Data;
using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APILivraria.Repositories;

public class LivrariaRepositorie : ILivrariaRepositorie
{
    private readonly LivrariaContext context;

    public LivrariaRepositorie(LivrariaContext context)
    {
        this.context = context;
    }

    public async Task<string> AdicionarLivro(Livro livraria, CancellationToken cancellationToken)
    {
        await context.Livrarias.AddAsync(livraria, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return "Cadastrado";
    }

    public async void ApagarLivro(Livro livro)
    {
        context.Livrarias.Remove(livro);
        await context.SaveChangesAsync();
    }

    public async Task<Livro?> FiltrarLivroById(int id)
    {
        var livrarias = await context.Livrarias.FirstOrDefaultAsync(l => l.Id == id);

        return livrarias;
    }

    public ListaDeLivros ObterTodosLivros(int paginaAtual, int quantidadeItensPagina, decimal? precoEntreMin, decimal? precoEntreMax, List<int>? generoIds, OrdenacaoPreco? ordenacaoPreco)
    {
        var livrosFiltrados = context.Livrarias.Where(l => l.Quantidade != 0);

        if (precoEntreMin != null && precoEntreMax != null)
        {
            livrosFiltrados = livrosFiltrados.Where(l => l.Preco >= precoEntreMin && l.Preco <= precoEntreMax);
        }

        if (generoIds != null)
        {
            livrosFiltrados = livrosFiltrados.Where(lvro => lvro.Generos!.All(genero => generoIds.Contains(genero.GeneroId)));
        }

        if (ordenacaoPreco == OrdenacaoPreco.MenorParaMaior)
        {
            livrosFiltrados = livrosFiltrados.OrderBy(livrosFiltrados => livrosFiltrados.Preco);
        }

        else if (ordenacaoPreco == OrdenacaoPreco.MaiorParaMenor)
        {
            livrosFiltrados = livrosFiltrados.OrderByDescending(livrosFiltrados => livrosFiltrados.Preco);
        }

        var PaginasPassadas = paginaAtual - 1;

        var PaginasSkip = quantidadeItensPagina * PaginasPassadas;

        var Listadelivros = new ListaDeLivros
        {
            Livros = livrosFiltrados.Skip(PaginasSkip).Take(quantidadeItensPagina),
            TotalLivros = livrosFiltrados.Count(),
        };

        return Listadelivros;

    }

    public bool LivroCarrinhoJaAdicionadoAntes(int userId, int livroId)
    {
        return context.CarrinhoItems
                      .Include(ci => ci.Carrinho)
                      .Any(ci => ci.Carrinho!.UserId == userId && ci.LivroId == livroId);
    }


    public async Task AddCarrinhoItem(CarrinhoItem carrinhoItem, int userid, int livroid)
    {

        var user = await context.Users.Include(u => u.Carrinho).ThenInclude(c => c!.CarrinhoItens).FirstAsync(u => u.Id == userid);
        var carrinhoid = user.Carrinho!.CarrinhoId;
        var livroJaAdicionadoNoCarrinho = LivroCarrinhoJaAdicionadoAntes(userid, livroid);

        if (livroJaAdicionadoNoCarrinho)
        {
            var carrinhoItemExistente = user.Carrinho
                                            .CarrinhoItens
                                            .FirstOrDefault(ci => ci.LivroId == livroid);

            carrinhoItemExistente!.Quantidade += carrinhoItem.Quantidade;

            context.Update(carrinhoItemExistente);
        }
        else
        {
            await context.AddAsync(carrinhoItem);
        }

        await context.SaveChangesAsync();
    }

    public async Task<CarrinhoItemDtoPreco> ReturnLivrosCarrinhosByUserId(int Userid)
    {
        var user = await context.Users
                               .Include(u => u.Carrinho)
                               .ThenInclude(c => c!.CarrinhoItens)
                               .ThenInclude(c => c.Livro)
                               .FirstAsync(u => u.Id == Userid);

        var carrinho = user.Carrinho;
        var listaItems = carrinho!.CarrinhoItens.ToList();

        var carrinhoItems = new List<CarrinhoItemDto>();

        var itemPreco = decimal.Zero;
        foreach (var item in listaItems)
        {
            var livros = item.Livro;
            var quantidade = item.Quantidade;

            var carrinhoitem = new CarrinhoItemDto
            {
                Livro = livros!.Nome,
                Quantidade = quantidade,
                Preco = livros.Preco,
            };

            itemPreco += livros.Preco * quantidade;
            carrinhoItems.Add(carrinhoitem);
        }
        var carrinhoitemPreco = new CarrinhoItemDtoPreco
        {
            CarrinhoItemsDto = carrinhoItems,
            PrecoTotal = itemPreco,
            QuantidadeTotal = carrinho.QuantidadeItens
        };

        return carrinhoitemPreco;
    }

    public async Task<string> DeleteLivroCarrinho(int livroQuantidade, int userId, int livroId, bool apagarTodos)
    {
        var user = await context.Users
                                .Include(u => u.Carrinho)
                                .ThenInclude(c => c!.CarrinhoItens)
                                .ThenInclude(ci => ci.Livro)
                                .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null || user.Carrinho == null)
        {
            return "Usuário ou carrinho não encontrado.";
        }

        var carrinho = user.Carrinho;
        var itemToRemove = carrinho.CarrinhoItens
                                   .FirstOrDefault(ci => ci.LivroId == livroId);

        if (apagarTodos)
        {
            context.CarrinhoItems.RemoveRange(carrinho.CarrinhoItens);

            carrinho.CarrinhoItens.Clear();
        }

        else if (itemToRemove?.Quantidade > livroQuantidade)
        {
            itemToRemove.Quantidade -= livroQuantidade;
            context.CarrinhoItems.Update(itemToRemove);
        }

        await context.SaveChangesAsync();

        return "Livro removido da lista";
    }

    public async Task<string> FinalizarCompraCarrinho(int userId, bool finalizarCompra)
    {
        var user = await context.Users
                                .Include(u => u.Carrinho)
                                .ThenInclude(c => c!.CarrinhoItens)
                                .ThenInclude(ci => ci.Livro)
                                .FirstAsync(u => u.Id == userId);

        var carrinho = user.Carrinho;

        if (finalizarCompra)
        {
            context.CarrinhoItems.RemoveRange(carrinho!.CarrinhoItens);

            carrinho.CarrinhoItens.Clear();
        }
        await context.SaveChangesAsync();

        return "Compra finalizada";
    }
}
