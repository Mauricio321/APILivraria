using APILivraria.DTOs;
using APILivraria.Models;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services;

namespace APILivraria.Tests.Testes
{
    public class LivroTestes
    {
        private readonly Mock<ILivrariaRepositorie> mockLivroTest = new();
        private readonly Mock<IUserRepositorie> mockUserTest = new();
        private readonly Mock<IGenerosRepositories> mockGeneroTest = new();
        private readonly UserService usuarioService;
        private readonly LivrariaService livroService;
        private readonly GeneroService generoService;

        public LivroTestes()
        {
            livroService = new(mockLivroTest.Object, mockUserTest.Object, mockGeneroTest.Object);
        }

        [Fact]
        public async Task AddLivroSucesso()
        {
            //Arrange
            var generoIds = new List<int> { 1 };

            var genero = new Generos { Nome = "Ficção", Id = 1 };

            var generos = new List<Generos> { genero };


            var request = new LivrariaSemId
            {
                Livro = "Chapeuzinho Vermelho",
                Autor = "Charles Perrault",
                Preco = 100,
                Quantidade = 1,
                GeneroIds = generoIds
            };

            var livroAdicionado = new LivroAdicionado
            {
                Mensagem = "Livro adicionado"
            };

            mockLivroTest.Setup(l => l.AdicionarLivro(It.IsAny<Livro>(), CancellationToken.None)).ReturnsAsync(livroAdicionado);

            string generoAdicionado = "genero adicionado";

            mockGeneroTest.Setup(l => l.AdicionarGenero(It.IsAny<Generos>())).ReturnsAsync(generoAdicionado);
            mockGeneroTest.Setup(l => l.ObterGenero(generoIds, CancellationToken.None)).ReturnsAsync(generos);

            //Act
            var envelopeDeLivroAdicionado = await livroService.AdicionarLivro(request, CancellationToken.None);

            //Assert
            Assert.True(envelopeDeLivroAdicionado.DeuCerto);
            Assert.Null(envelopeDeLivroAdicionado.MensagemErro);
            Assert.Equal(envelopeDeLivroAdicionado.Conteudo, livroAdicionado);
        }

        [Fact]

        public async Task AddLivroCasoFalha() 
        {
            //Arrange
            var generoIds = new List<int> { 1 };

            var genero = new Generos { Nome = "Ficção", Id = 2 };

            var generos = new List<Generos> { genero };


            var request = new LivrariaSemId
            {
                Livro = "Chapeuzinho Vermelho",
                Autor = "Charles Perrault",
                Preco = 100,
                Quantidade = 1,
                GeneroIds = generoIds
            };

            var livroAdicionado = new LivroAdicionado
            {
                Mensagem = "Livro adicionado"
            };

            mockLivroTest.Setup(l => l.AdicionarLivro(It.IsAny<Livro>(), CancellationToken.None)).ReturnsAsync(livroAdicionado);

            string generoAdicionado = "genero adicionado";

            mockGeneroTest.Setup(l => l.AdicionarGenero(It.IsAny<Generos>())).ReturnsAsync(generoAdicionado);
            mockGeneroTest.Setup(l => l.ObterGenero(generoIds, CancellationToken.None)).ReturnsAsync(generos);

            //Act
            var envelopeDeLivroAdicionado = await livroService.AdicionarLivro(request, CancellationToken.None);

            //Assert
            Assert.False(envelopeDeLivroAdicionado.DeuCerto);
            Assert.NotNull(envelopeDeLivroAdicionado.MensagemErro);
            Assert.Null(envelopeDeLivroAdicionado.MensagemDeuCerto);
        }
    }
}
