using APILivraria.Models;

namespace APILivraria.Repositories.Interfaces
{
    public interface IUserRepositorie
    {
        void AddUser(User user);

        List<User> Get();

        User? GetUser(string email, string password);

        bool EmailExistente(string email);

        bool IdExistente(int id);

        void DeleteUser(int id);
        string DeleteOtherUsers(int id);
    }
}
