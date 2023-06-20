namespace WeAreDevs.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Conta Conta { get; set; }
        public ICollection<UsuarioCurso> Curso { get; set; }
    }
}