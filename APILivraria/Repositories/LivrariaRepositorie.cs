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

    public async Task<LivroAdicionado> AdicionarLivro(Livro livraria, CancellationToken cancellationToken)
    {
        await context.Livrarias.AddAsync(livraria, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new LivroAdicionado
        {
            Mensagem = "livro cadastrado"
        };
    }

    public void ApagarLivro(Livro livro)
    {
        context.Livrarias.Remove(livro);
    }

    public void SaveChangesAsync()
    {
        context.SaveChangesAsync();
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


    public async Task CarrinhoItemUpdate(CarrinhoItem carrinhoItem)
    {
        context.Update(carrinhoItem);

        await context.SaveChangesAsync();
    }

    public async Task AddCarrinhoItem(CarrinhoItem carrinhoItem)
    {
        await context.AddAsync(carrinhoItem);

        await context.SaveChangesAsync();
    }

    public async Task<User> FinalizarCompraCarrinho(int userId)
    {
        return await context.Users
                            .Include(u => u.Carrinho)
                            .ThenInclude(c => c!.CarrinhoItens)
                            .ThenInclude(ci => ci.Livro)
                            .FirstAsync(u => u.Id == userId);
    }

    public void RemoveCarrinhoItens(IEnumerable<CarrinhoItem> carrinhoItens)
    {
        context.CarrinhoItems.RemoveRange(carrinhoItens);

        context.SaveChanges();
    }

    public void UpdateCarrinhoItem(CarrinhoItem item)
    {
        context.CarrinhoItems.Update(item);
    }
}
