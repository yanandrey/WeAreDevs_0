namespace WeAreDevs.Models
{
    public class Topico
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Nivel { get; set; }
        public Curso Curso { get; set; }
    }
}