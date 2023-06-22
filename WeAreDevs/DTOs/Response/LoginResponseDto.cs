namespace WeAreDevs.DTOs.Response
{
    public class LoginResponseDto
    {
        public string TokenDeAcesso { get; set; }
        public DateTime DataDeExpiracao { get; set; }
    }
}