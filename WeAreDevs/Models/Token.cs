namespace WeAreDevs.Models
{
    public class Token
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpiracaoDoTokenDeAcesso { get; set; }
    }
}