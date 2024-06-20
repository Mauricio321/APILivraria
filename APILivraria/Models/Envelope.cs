namespace APILivraria.Models
{
    public class Envelope<T>
    {
        public T? Conteudo { get; set; }
        public bool DeuCerto { get; set; }
        public string? MensagemErro { get; set; }
        public TiposDeErro TipoDeErro { get; set; }
    }

    public enum TiposDeErro
    {
        BadRequest = 0,
        NotFound = 1,
        Forbidden = 2,
    }
}
