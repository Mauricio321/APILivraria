using APILivraria.Models;

namespace APILivraria.Repositories.Interfaces
{
    public interface IUserRepositorie
    {
        void AddUser(User user);

        List<User> Get();

        User? GetUser(string email);

        bool EmailExistente(string email);

        bool IdExistente(int id);

        void DeleteUser(int id);
        string DeleteOtherUser(int id);
        Task<User?> GetCarrinhoByUserId(int userId);
        Task<User?> GetUserById(int userId);
    }
}
