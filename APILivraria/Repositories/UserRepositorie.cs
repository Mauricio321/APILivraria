using APILivraria.Data;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APILivraria.Repositories;

public class UserRepositorie : IUserRepositorie
{
    private readonly LivrariaContext context;
    public UserRepositorie(LivrariaContext context)
    {
        this.context = context;
    }

    public void AddUser(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }

    public List<User> Get()
    {
        return context.Users.ToList();
    }

    public User? GetUser(string email)
    {
        return context.Users.Include(r => r.Role).FirstOrDefault(u => u.Email == email);
    }

    public Task<User?> GetCarrinhoByUserId(int userId)
    {
        return context.Users.Include(c => c.Carrinho).ThenInclude(c => c.CarrinhoItens).FirstOrDefaultAsync(u => u.Id == userId);
    }

    public Task<User?> GetUserById(int userId)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }


    public bool EmailExistente(string email)
    {
        return context.Users.Any(u => u.Email == email);
    }

    public bool IdExistente(int id)
    {
        return context.Users.Any(i => i.Id == id);
    }

    public void DeleteUser(int id)
    {
        var apagarUsusario = context.Users.First(u => u.Id == id);

        context.Users.Remove(apagarUsusario);
        context.SaveChanges();
    }

    public string DeleteOtherUser(int id)
    {
        var apagaroutrosusuarios = context.Users.FirstOrDefault(u => u.Id == id);

        context.Users.Remove(apagaroutrosusuarios!);

        context.SaveChanges();

        return "usuario deletado";
    }
}
