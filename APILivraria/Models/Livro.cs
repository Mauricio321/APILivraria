namespace APILivraria.Models;

public class Livro : Entity
{
    public string Nome { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }

    public IList<LivroGenero> Generos { get; set; } = default!;
    public List<User> Usuarios { get; set; } = default!;
}

public enum OrdenacaoPreco
{
    MenorParaMaior = 1,  
    MaiorParaMenor = 2
}



