using APILivraria.Models;

namespace APILivraria.Tests.Testes
{
    internal class Envelope
    {
        public object Conteudo { get; set; }
        public object MensagemDeuCerto { get; set; }
        public string MensagemErro { get; set; }
        public TiposDeErro TipoDeErro { get; set; }
    }
}