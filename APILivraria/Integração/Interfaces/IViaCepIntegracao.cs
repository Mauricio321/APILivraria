using apilivraria.models;
using APILivraria.Integração.Respoonse;

namespace APILivraria.Integração.Interfaces
{
    public interface IViaCepIntegracao
    {
        Task<CadastroCompra> ObterDadosViaCep(string cep, CadastroCompraDto dto);
    }
}
