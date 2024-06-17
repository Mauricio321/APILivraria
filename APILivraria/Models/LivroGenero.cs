namespace APILivraria.Models;

public class LivroGenero
{
    public Livro Livro { get; set; } = default!;
    public int LivroId { get; set; }
    public Generos Genero { get; set; } = default!; 
    public int GeneroId { get; set; }
}
