namespace APILivraria.DTOs
{
    public class CarrinhoItemDtoPreco
    {
        public IEnumerable<CarrinhoItemDto>? CarrinhoItemsDto { get; set; }
        public decimal PrecoTotal { get; set; }
        public int QuantidadeTotal { get; set; }
    }
}
