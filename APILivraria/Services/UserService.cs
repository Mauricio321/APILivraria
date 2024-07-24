using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;
using APILivraria.ViewModel;
using System;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace APILivraria.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepositorie userRepositorie;
        public UserService(IUserRepositorie userRepositorie)
        {
            this.userRepositorie = userRepositorie;
        }


        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private string HashPassword(string password, out byte[] salt)
        { 
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }
        
        private string HashPassword(string password, byte[] salt)
        { 
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        public Envelope<ManagerViewModel> AddManager(ManagerViewModel managerView)
        {
            const int RoleIdManager = 1;

            var user = new User
            {
                Email = managerView.Email,
                Password = HashPassword(managerView.Password, out var salt),
                RoleId = RoleIdManager,
                Salt = salt,
                Carrinho = new Carrinho { }
            };

            string Passwordpattern = @"[@#%&$]";
            var containsSpecialChar = Regex.IsMatch(managerView.Password, Passwordpattern);

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var containsemailPattern = Regex.IsMatch(managerView.Email, emailPattern);

            var emailExistente = userRepositorie.EmailExistente(managerView.Email);

            if (emailExistente || !containsemailPattern)
            {
                return new Envelope<ManagerViewModel> { MensagemErro = "Email invalido, tente novamente", TipoDeErro = TiposDeErro.Forbidden };
            }

            if (managerView.Password.Length < 8 || !containsSpecialChar)
            {
                return new Envelope<ManagerViewModel> { MensagemErro = "A senha deve ter pelo menos 8 caracteres e um caractere especial", TipoDeErro = TiposDeErro.BadRequest };
            }

            userRepositorie.AddUser(user);
            return new Envelope<ManagerViewModel> { MensagemDeuCerto = "usuario adicionado", Conteudo = managerView };
        }

        public Envelope<UserViewModel> AddUser(UserViewModel userView)
        {
            const int RoleIdUser = 2;

            var user = new User
            {
                Email = userView.Email,
                Password = HashPassword(userView.Password, out var salt),
                RoleId = RoleIdUser,
                Salt = salt,
                Carrinho = new Carrinho { }
            };

            string Passwordpattern = @"[@#%&$]";
            var PasswordContainsSpecialChar = Regex.IsMatch(userView.Password, Passwordpattern);

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var containsemailPattern = Regex.IsMatch(userView.Email, emailPattern);

            string passwordNumbers = @"\d";
            var passwordContainsNumber = Regex.IsMatch(userView.Password, passwordNumbers);

            string passwordCapitaLetter = @"[A-Z]";
            var passwordContainsCapitalLetter = Regex.IsMatch(userView.Password, passwordCapitaLetter);

            var emailExistente = userRepositorie.EmailExistente(userView.Email);

            if (emailExistente || !containsemailPattern)
            {
                return new Envelope<UserViewModel> { MensagemErro = "Email invalido, tente novamente", TipoDeErro = TiposDeErro.Forbidden };
            }

            if (userView.Password.Length < 8 || !PasswordContainsSpecialChar || !passwordContainsNumber || !passwordContainsCapitalLetter)
            {
                return new Envelope<UserViewModel> { MensagemErro = "A senha deve ter pelo menos 8 caracteres, um caractere especial, uma letra maiúscula e um numero", TipoDeErro = TiposDeErro.BadRequest };
            }

            userRepositorie.AddUser(user);
            return new Envelope<UserViewModel> { MensagemDeuCerto = "usuario adicionado", Conteudo = userView };
        }

        public Envelope<string> AuthUser(string email, string password)
        {
            var user = userRepositorie.GetUser(email);


            if (user == null)
            {
                return new Envelope<string> { MensagemErro = "Username or password is invalid", TipoDeErro = TiposDeErro.BadRequest };
            }         

            var hashedPassword = HashPassword(password, user.Salt);
            
            if (user.Password != hashedPassword) 
            {
                return new Envelope<string> { MensagemErro = "Username or password is invalid", TipoDeErro = TiposDeErro.BadRequest };
            }


            var token = TokenServices.GenerateToken(user);
            return new Envelope<string> { Conteudo = token };
        }

        public Envelope<string> DeleteOtherUser(int id)
        {
            userRepositorie.DeleteOtherUser(id);

            var usuarioExistente = userRepositorie.IdExistente(id);
            if (!usuarioExistente)
            {
                return new Envelope<string> { MensagemErro = "Usuario não encontrado", TipoDeErro = TiposDeErro.NotFound };
            }
            return new Envelope<string> { MensagemDeuCerto = "Usuario deletado" };
        }

        public Envelope<string> DeleteUser(int id)
        {
            userRepositorie.DeleteUser(id);

            var idExistente = userRepositorie.IdExistente(id);

            if (!idExistente)
            {
                return new Envelope<string> { MensagemErro = "Usuario nao encontrado", TipoDeErro = TiposDeErro.NotFound };
            }

            return new Envelope<string> { MensagemDeuCerto = "Usuario deletado" }; 
        }
    }
}
