using APILivraria.Models;

namespace APILivraria.DTOs
{
    public class ListaDeLivros
    {
       public required IEnumerable<Livro> Livros { get; set; }
       public int TotalLivros { get; set; }
    }
}

