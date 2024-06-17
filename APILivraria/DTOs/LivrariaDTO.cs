namespace APILivraria.DTOs
{
    public class LivrariaDTO
    {
        public int Id { get; set; }
        public string Livro { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public IEnumerable<string>? Generos { get; set; }
    }
}
