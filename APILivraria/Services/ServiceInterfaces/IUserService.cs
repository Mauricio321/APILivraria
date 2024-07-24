using APILivraria.Models;
using APILivraria.ViewModel;

namespace APILivraria.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Envelope<UserViewModel> AddUser(UserViewModel userView);
        Envelope<ManagerViewModel> AddManager(ManagerViewModel managerView);
        Envelope<string> DeleteUser(int id);
        Envelope<string> DeleteOtherUser(int id);
        Envelope<string> AuthUser(string email, string password);
    }
}
