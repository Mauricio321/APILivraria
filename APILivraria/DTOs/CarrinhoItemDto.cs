using APILivraria.Models;

namespace APILivraria.DTOs
{
    public class CarrinhoItemDto
    {
        public required string Livro { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}

