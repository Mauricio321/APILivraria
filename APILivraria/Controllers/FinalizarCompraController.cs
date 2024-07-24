using APILivraria.DTOs;
using APILivraria.Integracao.Interfaces;
using APILivraria.Integracao.Response;
using APILivraria.Repositories.Interfaces;
using APILivraria.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace APILivraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinalizarCompraController : ControllerBase
    {
        private readonly ILivrariaRepositorie livrariaRepositorie;
        private readonly ILivrariaService livrariaService;
        public FinalizarCompraController(ILivrariaRepositorie livrariaRepositorie, ILivrariaService livrariaService)
        {
            this.livrariaRepositorie = livrariaRepositorie;
            this.livrariaService = livrariaService;
        }

        [Authorize]
        [HttpDelete]
        public async Task<DeleLivrosCarrinhoDto> FinalizarCompraCarrinho(bool finalizarCompra)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = int.Parse(identity!.FindFirst("userId")!.Value);
            await livrariaService.FinalizarCompraCarrinho(id, finalizarCompra);
            
            var result = new DeleLivrosCarrinhoDto
            {
                Mensagem = "Compra finalizada com sucesso, seu livro chegará em em alguns dias"
            };

            return result;
        }
    }
}
