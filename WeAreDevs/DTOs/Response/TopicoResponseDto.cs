namespace WeAreDevs.DTOs.Response
{
    public class TopicoResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Nivel { get; set; }
    }
}