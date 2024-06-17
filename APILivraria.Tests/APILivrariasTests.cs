//using APILivraria.Data;
//using APILivraria.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Xunit;
//using System.Linq;
//using System.Collections.Generic;
//using APILivraria.Models;

//namespace APILivraria.Tests
//{
//    public class APILivrariasTests
//    {

//    public class LivrariaRepositoryTests 
//    {
//        [Fact]
//        public void ApagarLivroDeveRemoverLivrosComONomeEspecificado()
//        {
//            // Arrange
//            var options = new DbContextOptionsBuilder<LivrariaContext>()
//                .UseInMemoryDatabase(databaseName: "LivrariaTestDatabase")
//                .Options;

//            var context = new LivrariaContext(options);
//            var livro = context.Livrarias.Select(l => l.Livro);

//            using (context)
//            {

//                context.Livrarias.Add(new Livraria { Livro = livro });
//                context.Livrarias.Add(new Livraria { Livro = "Outro Livro" });
//                context.Livrarias.Add(new Livraria { Livro = "Livro Teste 2" });
//                context.SaveChanges();
//            }

//            using (context)
//            {
//                var repository = new LivrariaRepositorie(context);

//                // Act
//                repository.ApagarLivro("Livro Teste");

//                // Assert
//                var livrosRestantes = context.Livrarias.ToList();
//                Assert.Single(livrosRestantes);
//                Assert.DoesNotContain(livrosRestantes, l => l.Livro.Contains("Livro Teste"));
//            }
//        }
//    }

//}
//}