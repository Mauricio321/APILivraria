using APILivraria.Integracao.Interfaces;
using APILivraria.Integracao.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APILivraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoCompraController : ControllerBase
    {
        private readonly IViaCepRepository viaCepRepository;
        public EnderecoCompraController(IViaCepRepository viaCepRepository)
        {
            this.viaCepRepository = viaCepRepository;
        }
        [Authorize] 
        [HttpPost("{cep}")]
        public async Task<ActionResult<CadastroEnderecoCompra>> ListarDadosEndereco(string cep, CadastroEnderecoDto dto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = int.Parse(identity!.FindFirst("userId")!.Value);

            var responseData = await viaCepRepository.ObterDadosViaCep(cep, dto, id);
          
            if (responseData is null)
                return NotFound("cep não encontrado");

            return Ok(responseData);
        }
    }
}
