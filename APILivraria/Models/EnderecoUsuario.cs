using APILivraria.Integracao.Response;

namespace APILivraria.Models
{
    public class EnderecoUsuario
    {
        public CadastroEnderecoCompra? EnderecoCompra {  get; set; }
        public int EnderecoId { get; set; }
        public User Usuario { get; set; } = default!;
        public int UserId { get; set; }
    }
}
