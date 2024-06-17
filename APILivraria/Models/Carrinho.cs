namespace APILivraria.Models
{
    public class Carrinho
    {
        public int CarrinhoId { get; set; }
        public int QuantidadeItens => CarrinhoItens.Sum(item => item.Quantidade);
        public User Usuario { get; set; } = default!;
        public int UserId { get; set; }
        public List<CarrinhoItem> CarrinhoItens { get; set; } = new List<CarrinhoItem>();
    }
}
