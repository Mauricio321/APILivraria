using APILivraria.Models;

namespace APILivraria.Services.ServiceInterfaces
{
    public interface IUserService
    {
        void AddUser(User user);

        List<User> Get();

        User? GetUser(string email, string password);

        bool EmailExistente(string email);

        bool IdExistente(int id);

        void DeleteUser(int id);
        string DeleteOtherUsers(int id);
        Task<User?> GetUserById(int userId);
    }
}
