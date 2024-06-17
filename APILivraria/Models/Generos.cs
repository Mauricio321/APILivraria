namespace APILivraria.Models
{
    public class Generos : Entity
    {
        public required string Nome { get; set; }
        public  IList<LivroGenero>? Livrosss { get; set; }
    }
}
