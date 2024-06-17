using apilivraria.models;
using APILivraria.Integração.Interfaces;
using APILivraria.Integração.Refit;
using APILivraria.Integração.Respoonse;
using Refit;

namespace APILivraria.Integração
{
    public class ViaCepIntegracao : IViaCepIntegracao
    {
        private readonly ICadastroCompraRepositorie cadastroCompraRepositorie;
        public ViaCepIntegracao(ICadastroCompraRepositorie cadastroCompraRepositorie)
        {
            this.cadastroCompraRepositorie = cadastroCompraRepositorie;
        }
        public async Task<CadastroCompra> ObterDadosViaCep(string cep, CadastroCompraDto dto)
        {
            var responseData = await cadastroCompraRepositorie.ObterDadosCadastroCompra(cep, dto);

            if (EhStatusDeFalha(responseData))
            {
                return null;
            }

            var content = responseData.Content;

            return responseData.Content;
        }

        private static bool EhStatusDeFalha(ApiResponse<CadastroCompra> response) => !response.IsSuccessStatusCode;
    }
}
