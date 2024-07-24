using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services;
using APILivraria.ViewModel;

namespace APILivraria.Tests.Testes;

public class UsuarioServiceTests
{
    private readonly Mock<IUserRepositorie> mockUserTest = new();
    private readonly UserService usuarioService;

    public UsuarioServiceTests()
    {
        usuarioService = new(mockUserTest.Object);
    }

    [Theory]
    [InlineData("emailinvalido.com")] //Email sem @
    [InlineData("emailinvalido@gmail")] //Email sem .
    public void TesteAdicaoUusarioEmailInvalido(string email)
    {
        //Arrange
        const string SenhaValida = "Emailinvalido@1";

        var viewModel = new UserViewModel
        {
            Email = email,
            Password = SenhaValida
        };

        mockUserTest.Setup(e => e.EmailExistente(email)).Returns(false);

        //Act
        var envelopeEmailInvalido = usuarioService.AddUser(viewModel);

        //Assert
        Assert.False(envelopeEmailInvalido.DeuCerto);
        Assert.Null(envelopeEmailInvalido.MensagemDeuCerto!);
        Assert.NotNull(envelopeEmailInvalido.MensagemErro);
        Assert.NotEmpty(envelopeEmailInvalido.MensagemErro);
    }

    [Theory]
    [InlineData("Senha1@")] //Senha com menos de 8 caracteres 
    [InlineData("Senha123")] //Senha sem caractere especial
    [InlineData("Senha@#$")]//Senha sem numero
    [InlineData("senha123@")]//Senha sem letra maiuscula
    public void TesteAdicaoUsuarioSenhaInvalida(string senha)
    {
        //Arange
        const string EmailValido = "emailvalido@gmail.com";

        var userView1 = new UserViewModel
        {
            Email = EmailValido,
            Password = senha
        };

        var request3 = new User
        {
            Email = userView1.Email,
            Password = userView1.Password
        };

        mockUserTest.Setup(u => u.AddUser(request3));

        //Act
        var envelopeSenhaMenosDeOitoCaracteres = usuarioService.AddUser(userView1);

        //Assert
        Assert.False(envelopeSenhaMenosDeOitoCaracteres.DeuCerto);
        Assert.Null(envelopeSenhaMenosDeOitoCaracteres.MensagemDeuCerto!);
        Assert.NotNull(envelopeSenhaMenosDeOitoCaracteres.MensagemErro);
        Assert.NotEmpty(envelopeSenhaMenosDeOitoCaracteres.MensagemErro);
    }

    [Fact]
    public void TesteAdicaoUsuarioValido()
    {
        // Arrange
        const string EmailValido = "teste@test.com";
        const string SenhaForte = "Test@12345";

        var request = new UserViewModel
        {
            Email = EmailValido,
            Password = SenhaForte
        };

        mockUserTest.Setup(userrepository => userrepository.EmailExistente(EmailValido)).Returns(false);

        // Act
        var envelopdeuserviewmodel = usuarioService.AddUser(request);

        // Assert
        Assert.True(envelopdeuserviewmodel.DeuCerto);
        Assert.Null(envelopdeuserviewmodel.MensagemErro);
        Assert.NotNull(envelopdeuserviewmodel.MensagemDeuCerto);
        Assert.NotEmpty(envelopdeuserviewmodel.MensagemDeuCerto!);

        mockUserTest.Verify(userrepository => userrepository.AddUser(It.IsAny<User>()));
    }

    [Fact]
    public void TesteAdicaoUsuarioComEmailExistente()
    {
        // Arrange
        const string EmailExistente = "teste@test.com";
        const string SenhaForte = "test@12345";

        var request = new UserViewModel
        {
            Email = EmailExistente,
            Password = SenhaForte
        };

        mockUserTest.Setup(userrepository => userrepository.EmailExistente(EmailExistente)).Returns(true);

        // Act
        var envelopdeuserviewmodel = usuarioService.AddUser(request);

        // Assert
        Assert.False(envelopdeuserviewmodel.DeuCerto);
        Assert.NotNull(envelopdeuserviewmodel.MensagemErro);
        Assert.NotEmpty(envelopdeuserviewmodel.MensagemErro!);
        Assert.Null(envelopdeuserviewmodel.MensagemDeuCerto);
    }
}
