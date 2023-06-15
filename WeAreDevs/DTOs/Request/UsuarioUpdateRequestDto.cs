namespace WeAreDevs.DTOs.Request
{
    public class UsuarioUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}