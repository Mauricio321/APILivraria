namespace APILivraria.Models
{
    public class Envelope<T>
    {
        public T? Conteudo { get; set; }
        public bool DeuCerto => MensagemErro == null || TipoDeErro is 0;
        public string? MensagemErro { get; set; }
        public string? MensagemDeuCerto { get; set; }
        public TiposDeErro TipoDeErro { get; set; }
    }

    public enum TiposDeErro
    {
        BadRequest = 1,
        NotFound = 2,
        Forbidden = 3,
    }
}
