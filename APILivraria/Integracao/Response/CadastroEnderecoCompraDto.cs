namespace APILivraria.Integracao.Response
{
    public class CadastroEnderecoCompraDto
    {
        public required string Cep { get; set; }
        public required string Localidade { get; set; }
        public required string Bairro { get; set; }
        public required string Uf { get; set; }
        public required string Ddd { get; set; }
    }
}
