namespace WeAreDevs.DTOs.Request
{
    public class UsuarioRequestDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public bool Status { get; set; }
        public List<Guid> Cursos { get; set; }
    }
}