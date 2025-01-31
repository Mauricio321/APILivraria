﻿using APILivraria.DTOs;
using APILivraria.Integracao.Interfaces;
using APILivraria.Integracao.Response;
using APILivraria.Repositories.Interfaces;
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
        public FinalizarCompraController(ILivrariaRepositorie livrariaRepositorie)
        {
            this.livrariaRepositorie = livrariaRepositorie;
        }

        [Authorize]
        [HttpDelete]
        public async Task<DeleLivrosCarrinhoDto> FinalizarCompraCarrinho(bool finalizarCompra)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var id = int.Parse(identity!.FindFirst("userId")!.Value);

            await livrariaRepositorie.FinalizarCompraCarrinho(id, finalizarCompra);
         
            var msg = $"Compra finalizada com sucesso, seu livro chegará em em alguns dias";
            
            var result = new DeleLivrosCarrinhoDto
            {
                Mensagem = msg,
            };

            return result;
        }
    }
}
