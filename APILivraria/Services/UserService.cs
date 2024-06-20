using APILivraria.Models;
using APILivraria.Services.ServiceInterfaces;

namespace APILivraria.Services
{
    public class UserService : IUserService
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public string DeleteOtherUsers(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool EmailExistente(string email)
        {
            throw new NotImplementedException();
        }

        public List<User> Get()
        {
            throw new NotImplementedException();
        }

        public User? GetUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public bool IdExistente(int id)
        {
            throw new NotImplementedException();
        }
    }
}
