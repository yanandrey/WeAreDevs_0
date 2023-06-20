namespace WeAreDevs.DTOs.Request
{
    public class CursoUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
    }
}