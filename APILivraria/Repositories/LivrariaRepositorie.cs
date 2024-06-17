using APILivraria.Controllers;
using APILivraria.Data;
using APILivraria.DTOs;
using APILivraria.Migrations;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Text;

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
        await context.Livro.AddAsync(livraria, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return "Cadastrado";
    }

    public void ApagarLivro(Livro livro)
    {
        context.Livro.Remove(livro);

        context.SaveChanges();
    }

    public IEnumerable<Livro> GetLivroById(int Id)
    {
        var livrarias = context.Livro.Where(livro => livro.Id == Id);

        return livrarias;
    }



    public IEnumerable<Livro> ObterTodosLivros(int paginaAtual, int quantidadeItensPagina, decimal? precoEntreMin, decimal? precoEntreMax, List<int>? generoIds, OrdenacaoPreco? ordenacaoPreco)
    {
        var livrosFiltrados = context.Livro.Where(l => l.Quantidade != 0);


        return livrosFiltrados;

    }

    public decimal LivroCarrinhoJaAdicionadoAntesReturnPreco(IEnumerable<int> livroId)
    {
        foreach (int id in livroId)
        {
            var livrojaadicionado = context.CarrinhoItems.Include(l => l.Livro).FirstOrDefault(l => l.LivroId == id);

            if (livrojaadicionado != null)
            {
                return livrojaadicionado.PrecoItem;
            }
        }

        return 0;
    }

    public bool LivroCarrinhoJaAdicionadoAntes(int userId, int livroId)
    {
        return context.CarrinhoItems
                      .Include(ci => ci.Carrinho)
                      .Any(ci => ci.Carrinho!.UserId == userId && ci.LivroId == livroId);
    }


    public async Task<string> AddCarrinhoItem(CarrinhoDto dto, int userid, int livroid)
    {

        var user = await context.Users.Include(u => u.Carrinho).ThenInclude(c => c!.CarrinhoItens).FirstAsync(u => u.Id == userid);
        var carrinhoid = user.Carrinho!.CarrinhoId;
        var livroJaAdicionadoNoCarrinho = LivroCarrinhoJaAdicionadoAntes(userid, livroid);

        if (livroJaAdicionadoNoCarrinho)
        {
            var carrinhoItemExistente = user.Carrinho
                                            .CarrinhoItens
                                            .FirstOrDefault(ci => ci.LivroId == livroid);

            carrinhoItemExistente!.Quantidade += dto.QuantidadeItens;

            context.Update(carrinhoItemExistente);
        }
        else
        {
            var carrinhoitem = new CarrinhoItem
            {
                CarrinhoId = carrinhoid,
                LivroId = dto.LivroId,
                Quantidade = dto.QuantidadeItens,
            };
            context.Update(carrinhoitem);
        }

        await context.SaveChangesAsync();

        return "livro adicionado";
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


