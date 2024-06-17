namespace APILivraria.Models
{
    public class CarrinhoItem
    {
        public int CarrinhoItemId { get; set; }
        public Carrinho Carrinho { get; set; } = default!;
        public int CarrinhoId { get; set; }
        public int LivroId { get; set; }
        public Livro? Livro { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoItem { get; set; }
        public decimal PrecoTotal { get; set; }
    }
}
