using APILivraria.Models;

namespace APILivraria.Integracao.Response
{
    public class CadastroEnderecoCompra
    {
        public int Id { get; set; }
        public required string PaisRegião { get; set; }
        public required string NomeCompleto { get; set; }
        public required string Telefone { get; set; }
        public required string Cep { get; set; }
        public required string Localidade { get; set; }
        public required string Bairro { get; set; }
        public required string Uf { get; set; }
        public required string Ddd { get; set; }
        public ICollection<EnderecoUsuario> Usuarios { get; set; } = Array.Empty<EnderecoUsuario>();
    }
}
