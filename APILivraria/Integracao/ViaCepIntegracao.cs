using APILivraria.Integracao.Interfaces;
using APILivraria.Integracao.Refit;
using APILivraria.Integracao.Response;
using APILivraria.Repositories;
using System.Security.Claims;

namespace APILivraria.Integracao
{
    public class ViaCepIntegracao : IViaCepRepository
    {
        private readonly IViaCepIntegracaoRefit context;
        public ViaCepIntegracao(IViaCepIntegracaoRefit context)
        {
            this.context = context;
        }
       
        public async Task<CadastroEnderecoCompra?> ObterDadosViaCep(string cep, CadastroEnderecoDto enderecoDto, int id)
        {
            var responseData = await context.ObterDadosViaCep(cep);

            if (!responseData.IsSuccessStatusCode)
            {
                return null;
            }

            var dto = responseData.Content;

            return new CadastroEnderecoCompra
            {
                NomeCompleto = enderecoDto.NomeCompleto,
                PaisRegião = enderecoDto.PaisRegião,
                Telefone = enderecoDto.Telefone,
                Bairro = dto.Bairro,
                Cep = cep,
                Ddd = dto.Ddd,
                Localidade = dto.Localidade,
                Uf = dto.Uf,
            };
        }
    }
}
