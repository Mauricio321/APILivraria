using APILivraria.Integracao.Response;
using Refit;

namespace APILivraria.Integracao.Refit
{
    public interface IViaCepIntegracaoRefit
    {
        [Get("/ws/{cep}/json")]
        Task<ApiResponse<CadastroEnderecoCompraDto>> ObterDadosViaCep(string cep);
    }
}
