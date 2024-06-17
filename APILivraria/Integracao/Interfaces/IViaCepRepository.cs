using APILivraria.Integracao.Response;

namespace APILivraria.Integracao.Interfaces
{
    public interface IViaCepRepository
    {
        Task<CadastroEnderecoCompra?> ObterDadosViaCep(string cep, CadastroEnderecoDto cadastroDto, int id);
    }
}
