using APILivraria.DTOs;

namespace APILivraria.NovaPasta2
{
    public class LivrariaSemId
    {
        public string Livro { get; set; } = string.Empty;
        public required List<int> GeneroIds { get; set; }
        public string Autor { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
