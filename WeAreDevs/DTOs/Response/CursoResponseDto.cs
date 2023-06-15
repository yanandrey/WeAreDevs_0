namespace WeAreDevs.DTOs.Response
{
    public class CursoResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
    }
}