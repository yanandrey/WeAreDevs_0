namespace WeAreDevs.DTOs.Response
{
    public class UsuarioResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public bool Status { get; set; }
    }
}