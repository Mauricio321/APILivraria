using apilivraria.models;
using APILivraria.Integração.Respoonse;
using Refit;

namespace APILivraria.Integração.Refit
{
    public interface ICadastroCompraRepositorie
    {
        [Get("/ws/{cep}/json/")]
        Task<ApiResponse<CadastroCompra>> ObterDadosCadastroCompra(string cep, CadastroCompraDto dto);
    }
}
